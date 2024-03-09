using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Controllers.Common;
using ToDoAPI.Repository.IRepository;
using Utils.Shared.DTO;
using Utils.Shared.Helper;

namespace ToDoAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationUserController : BaseController
    {
        private readonly IAministrationUserRepository _userRepo;
        protected APIResponse _response;
        private readonly ILogger<AdministrationUserController> _logger;
        public AdministrationUserController(IAministrationUserRepository userrepository,
                ILogger<AdministrationUserController> logger)
        {
            _userRepo = userrepository;
            _logger = logger;
            this._response = new();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO model)
        {
            try
            {
                if (ValidationHelper.IsModelEmpty(model))
                {
                    return await BadRequestResponse("Empty Usernameor Password");
                }
                var loginresponse = await _userRepo.Login(model);

                if (ValidationHelper.IsModelEmpty(loginresponse, loginresponse.User))
                {
                    return await BadRequestResponse("Username PAssword Is Incorrect");
                }
                return await SuccessRequestResponse(loginresponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await ExeptionError();
            }

        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<APIResponse>> Register([FromBody] RegistrationRequestDTO model)
        {
            try
            {
                if (ValidationHelper.IsModelEmpty(model))
                {
                    return await BadRequestResponse("Cannot be empty");
                }
                bool ifusernameunique = _userRepo.IsUniqueUser(model.UserName);
                if (!ifusernameunique)
                {
                    return await BadRequestResponse("Username Already exists");
                }
                var user = await _userRepo.Register(model);
                if (ValidationHelper.IsModelEmpty(user))
                {
                    return await BadRequestResponse("Passords or Username Doesnt meet the requirements");
                }
                return await SuccessRequestResponse<object>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exeption error");
                return await RegistrationFailed("Password Doesnt meet the requirement");
            }

        }
    }
}
