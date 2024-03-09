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
    public class ToDoListController : BaseController
    {
        private readonly IToDoListService _toDoListService;
        private readonly IUserService _userService;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ToDoListController> _logger;
        public ToDoListController(IToDoListService toDoListService,
            IMapper mapper, IUnitOfWork unitOFWork,
            ILogger<ToDoListController> logger, IUserService userService)
        {
            _toDoListService = toDoListService;
            _mapper = mapper;
            _unitOfWork = unitOFWork;
            _logger = logger;
            this._response = new();
            _userService = userService;
        }

        [HttpPost("CreateToDoList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> CreateToDoList([FromBody] ToDoListCreateDTO CreateDto)
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
                if (CreateDto.UserId == 0)
                {
                    return await BadRequestResponse("User Id Cannot be empty");
                }
                var user = await _userService.GetUsersById(CreateDto.UserId);
                if (ValidationHelper.IsModelEmpty(user))
                {
                    return await BadRequestResponse("User Not Found");
                }
                ToDoList mapToDoList = _mapper.Map<ToDoList>(CreateDto);
                mapToDoList.IsActive = SD.IsActive;
                mapToDoList.CreatedUserId = userId;
                var restaureant = await _toDoListService.CreateToDoList(mapToDoList);
                return await SuccessRequestResponse(restaureant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            }

        }

        [HttpPost("GetTodoListById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> GetTodoListById([FromHeader] int id)
        {
            try
            {
                if (id == 0)
                {
                    return await BadRequestResponse("Cannot be empty");
                }
                var toDoList = await _toDoListService.GetToDoListById(id);
                if (toDoList == null)
                {
                    return await BadRequestResponse("User  not found");
                }  
                return await SuccessRequestResponse(toDoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            } 
        }
        [HttpPost("GetAllToDoList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> GetAllToDoList()
        {
            try
            {
                var toDoList = await _toDoListService.GetAllToDoList();
                if (toDoList == null)
                {
                    return await BadRequestResponse("Users  not found");
                } 
                return await SuccessRequestResponse(toDoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            }

        }
       
        [HttpPost("UpdateToDoList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> UpdateToDoList([FromBody] ToDoListUpdateDTO updateDTO)
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
                var restaurant = await _toDoListService.GetToDoListById(updateDTO.Id);
                if (ValidationHelper.IsModelEmpty(restaurant))
                {
                    return await BadRequestResponse("Id not found");
                }
                if (restaurant.CreatedUserId != userId)
                {
                    return await BadRequestResponse("Only created user can Update the TodoList");
                }
              
                if (await _toDoListService.UpdateToDoList(updateDTO))
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

        [HttpPost("GetTodoListByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> GetTodoListByUserId([FromHeader] int id)
        {
            try
            {
                if (id == 0)
                {
                    return await BadRequestResponse("Cannot be empty");
                }
                var toDoList = await _toDoListService.GetToDoListByUserId(id);
                if (toDoList == null)
                {
                    return await BadRequestResponse("List  not found");
                }
                return await SuccessRequestResponse(toDoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            }
        }

        [HttpPost("DeleteToDoList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> DeleteToDoList([FromHeader] int id)
        {
            try
            {
                if (id == 0)
                {
                    return await BadRequestResponse("Cannot be empty");
                }
                var toDoList = await _toDoListService.GetToDoListById(id);
                if (toDoList == null)
                {
                    return await BadRequestResponse("List  not found");
                } 
                if (await _toDoListService.DeleteToDoList(id))
                {
                    return await SuccessRequestResponse("");
                }
                return await BadRequestResponse("Something went wrong");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            }
        }
    }
    }
