using AutoMapper;
using Domain.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoAPI.Controllers.Common;
using ToDoAPI.Repository.IRepository;
using Utils.Shared.DTO;
using Utils.Shared.Helper;

namespace ToDoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService,
            IMapper mapper, IUnitOfWork unitOFWork,
            ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _unitOfWork = unitOFWork;
            _logger = logger;
            this._response = new();
        }

        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> CreateUser([FromBody] UserCreateDto CreateDto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.GroupSid);
                if (ValidationHelper.IsModelEmpty(userId))
                {
                    return await BadRequestResponse("Not Valid Credentials");
                }
                if (ValidationHelper.IsModelEmpty(CreateDto))
                {
                    return await BadRequestResponse("Cannot be empty");
                }
                if (await _unitOfWork.User.GetAsync(u => u.UserName.ToLower() == CreateDto.UserName.ToLower()) != null)
                {
                    return await BadRequestResponse("UserName already used please try Different ");
                }
                User mapRestaurant = _mapper.Map<User>(CreateDto);
                mapRestaurant.IsActive = SD.IsActive;
                mapRestaurant.CreatedUserId = userId;
                var restaureant = await _userService.CreateUser(mapRestaurant);
                return await SuccessRequestResponse(restaureant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            }

        }

        [HttpPost("GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> GetUserById([FromHeader] int id)
        {
            try
            {
                if (id == 0)
                {
                    return await BadRequestResponse("Cannot be empty");
                }
                var user = await _userService.GetUsersById(id);
                if (user == null)
                {
                    return await BadRequestResponse("User  not found");
                }  
                return await SuccessRequestResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            } 
        }
        [HttpPost("GetAllUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> GetAllUser()
        {
            try
            {
                var user = await _userService.GetAllUsers();
                if (user == null)
                {
                    return await BadRequestResponse("Users  not found");
                } 
                return await SuccessRequestResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            }

        }
       
        [HttpPost("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> UpdateUser([FromBody] UserUpdateDto updateDTO)
        {
            try
            {                
                var userId = User.FindFirstValue(ClaimTypes.GroupSid);
                if (ValidationHelper.IsModelEmpty(userId))
                {
                    return await BadRequestResponse("Not Valid Credentials");
                }

                if (ValidationHelper.IsModelEmpty(updateDTO))
                {
                    return await BadRequestResponse("Empty Model");
                }
                if (updateDTO.Id == 0)
                {
                    return await BadRequestResponse("Id not valid");
                }
                var restaurant = await _userService.GetUsersById(updateDTO.Id);
                if (restaurant.CreatedUserId != userId)
                {
                    return await BadRequestResponse("Only created user can Update the user");
                }
                if (ValidationHelper.IsModelEmpty(restaurant))
                {
                    return await BadRequestResponse("Id not Found");
                }
                if (await _userService.UpdateUser(updateDTO))
                {
                    _unitOfWork.Save();
                    return await SuccessRequestResponse("");
                }
                return await BadRequestResponse("Something Went Wrong");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            }

        }
    }
    }
