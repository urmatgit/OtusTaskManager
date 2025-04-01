using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Business.Application.Projects.Commands.CreateProject;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    
    public class ProjectController : ApiController
    {
        private readonly ISender _sender;
        public ProjectController(ISender sender)
        {
            _sender = sender;    
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectCommand command)
        {
            var createResponse=await _sender.Send(command);
            return Ok(createResponse);
        }
    }
}
