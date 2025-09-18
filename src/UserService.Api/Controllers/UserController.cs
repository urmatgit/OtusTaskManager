using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using UserService.Business.Application.Projects.Commands.DeleteProject;
using UserService.Business.Application.Users.Commands.EditUser;
using UserService.Business.Application.Users.Queries;
using UserService.Business.Application.Users.Queries.GetAll;
using UserService.Business.Application.Users.Queries.GetById;
using UserService.DataAccess.Common;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ApiController
    {


        [HttpGet()]
        public async Task<IActionResult> GetUserAsync()
        {
            var result = await Mediator.Send(new GetUsersRequest());
            return Ok(result);
        }
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(Guid? id)
        {
            
           var result = await Mediator.Send(new GetUserProfileRequest(id));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ByIdAsync(Guid id)
        {
            var result =await Mediator.Send(new GetUserWithProjectsRequest(id));
            return Ok(result);
        }
        /// <summary>
        ///  меняем роль пользователя
        /// </summary>
        /// <param name="userRoleRequest"></param>
        /// <returns></returns>
        [HttpPost("changerole")]
        [Authorize("Admin")]
        public async Task<IActionResult> ChangeRoleAsync(ChangeUserRoleRequest userRoleRequest)
        {
            var result = await Mediator.Send(userRoleRequest);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("{id:guid}")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var result = await Mediator.Send(new DeleteUserRequest(id));
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
