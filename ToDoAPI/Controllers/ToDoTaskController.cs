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
    public class ToDoTaskController : BaseController
    {
        private readonly IToDoTaskService _todoTaskServide;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ToDoTaskController> _logger;
        public ToDoTaskController(IToDoTaskService toDoTaskService,
            IMapper mapper, IUnitOfWork unitOFWork,
            ILogger<ToDoTaskController> logger)
        {
            _todoTaskServide = toDoTaskService;
            _mapper = mapper;
            _unitOfWork = unitOFWork;
            _logger = logger;
            this._response = new();
        }

        [HttpPost("CreateToDoTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> CreateToDoTask([FromBody] ToDoTaskCreateDto CreateDto)
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
                
                TodoTask mapToDoTask = _mapper.Map<TodoTask>(CreateDto);
                mapToDoTask.IsActive = SD.IsActive;
                mapToDoTask.CreatedUserId = userId;
                var restaureant = await _todoTaskServide.CreateToDoTask(mapToDoTask);
                return await SuccessRequestResponse(restaureant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            }

        }

        [HttpPost("GetTodoTaskById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> GetTodoTaskById([FromHeader] int id)
        {
            try
            {
                if (id == 0)
                {
                    return await BadRequestResponse("Cannot be empty");
                }
                var toDoList = await _todoTaskServide.GetToDoTaskById(id);
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
        [HttpPost("GetAllToDoTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> GetAllToDoTask()
        {
            try
            {
                var toDoList = await _todoTaskServide.GetAllToDoTask();
                if (toDoList == null)
                {
                    return await BadRequestResponse("Tasks  not found");
                } 
                return await SuccessRequestResponse(toDoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            }

        }
       
        [HttpPost("UpdateToDoTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = SD.Role_BRBO)]
        public async Task<ActionResult<APIResponse>> UpdateToDoTask([FromBody] ToDoTaskUpdateDto updateDTO)
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
                var restaurant = await _todoTaskServide.GetToDoTaskById(updateDTO.Id);
                if (ValidationHelper.IsModelEmpty(restaurant))
                {
                    return await BadRequestResponse("Id not found");
                }
                if (restaurant.CreatedUserId != userId)
                {
                    return await BadRequestResponse("Only created user can Update the TodoList");
                }
              
                if (await _todoTaskServide.UpdateToDoTask(updateDTO))
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
