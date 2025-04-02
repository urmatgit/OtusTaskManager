using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Business.Application.Projects.Commands.CreateProject;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : ApiController
    {
        private readonly ISender _sender;
        public ProjectController(ISender sender)
        {
            _sender = sender;    
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectRequest command)
        {
            
            var createResponse=await _sender.Send(command);
            return Ok(createResponse);
        }
    }
}
