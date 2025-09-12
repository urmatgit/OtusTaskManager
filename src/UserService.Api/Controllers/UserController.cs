using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Business.Application.Projects.Commands.DeleteProject;
using UserService.Business.Application.Users.Commands.EditUser;
using UserService.Business.Application.Users.Queries;
using UserService.Business.Application.Users.Queries.GetAll;
using UserService.Business.Application.Users.Queries.GetById;

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
        [HttpPost]
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
