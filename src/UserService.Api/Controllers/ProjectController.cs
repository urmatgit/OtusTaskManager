using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Business.Application.Projects.Commands.CreateProject;
using UserService.Business.Application.Projects.Commands.DeleteProject;
using UserService.Business.Application.Projects.Commands.UpdateProject;
using UserService.Business.Application.Projects.Queries.GetAll;
using UserService.Business.Application.Projects.Queries.GetById;

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
        /// <summary>
        /// Получаем все проектек, кроме удаленных
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var result = await _sender.Send(new GetProjectsRequest());
            return Ok(result);
        }
        /// <summary>
        /// Получаем проек по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProjects(Guid id)
        {
            var result = await _sender.Send(new GetProjectByIdRequest(id)) ;
            if (result == null) { 
                return NotFound();
            }
            return Ok(result);
        }
        /// <summary>
        /// создание проекта
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectRequest request)
        {
            
            var createResponse=await _sender.Send(request);
            return Ok(createResponse);
        }
        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProject(UpdateProjectRequest request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var response = await _sender.Send(new DeleteProjectRequest(id));
            return Ok(response);
        }
    }
}
