using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ToDoAPI.Controllers.Common
{
    public class BaseController : ControllerBase
    {
        protected async Task<ActionResult<APIResponse>> BadRequestResponse(string errorMessage)
        {
            var response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            response.ErrorMessages.Add(errorMessage);

            return BadRequest(response);
        }
        protected async Task<ActionResult<APIResponse>> SuccessRequestResponse<T>(T result = default)
        {
            var response = new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = (result == null) ? "" : result
            };

            return Ok(response);
        }
        protected async Task<ActionResult<APIResponse>> ExeptionError()
        {
            var response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError
            };
            response.ErrorMessages.Add("Internal Server Error");

            return BadRequest(response);
        }
        protected async Task<ActionResult<APIResponse>> RegistrationFailed(string errorMsg)
        {
            var response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError
            };
            string errorMessage = (string.IsNullOrEmpty(errorMsg)) ? "Something Went Wrong" : errorMsg;
            response.ErrorMessages.Add(errorMessage);

            return BadRequest(response);
        }
        protected async Task<ActionResult<APIResponse>> NoRecordFound()
        {
            var response = new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = "No Records Found"
            }; 
            return Ok(response);
        }
    }
}
