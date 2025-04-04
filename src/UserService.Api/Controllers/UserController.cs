using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Business.Application.Users.Commands.EditUser;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ApiController
    {
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
    }
}
