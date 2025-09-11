using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Claims;
using UserService.Business.Application.Projects.Commands.CreateProject;
using UserService.Business.Application.Projects.Commands.DeleteProject;
using UserService.Business.Application.Projects.Commands.UpdateProject;
using UserService.Business.Application.Projects.Queries.GetAll;
using UserService.Business.Application.Projects.Queries.GetById;
using UserService.Business.Application.ProjectsUsers.Commands.AddUserToProject;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : ApiController
    {

        /// <summary>
        /// Получаем все проектек, кроме удаленных
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetProjectsAsync()
        {
            var request = new GetProjectsRequest();
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Получаем все проектек, кроме удаленных, через номера старицы и userid
        /// </summary>
        /// <returns></returns>
        [HttpGet("bypage")]
        public async Task<IActionResult> GetProjectsAsync(GetProjectsRequestByPage request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
        /// <summary>
        /// Получаем проек по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProjectsAsync(Guid id)
        {
            var result = await Mediator.Send(new GetProjectByIdRequest(id)) ;
            if (result.IsFailure) { 
                return NotFound();
            }
            return Ok(result.Value);
        }
        /// <summary>
        /// создание проекта
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProjectAsync(CreateProjectRequest request)
        {
            
            var createResponse=await Mediator.Send(request);
            if (createResponse.IsFailure)
            {
                return BadRequest(createResponse.Error);
            }
            return Ok(createResponse.Value);
        }
        /// <summary>
        /// Добавить пользователя проекта
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("adduser")]
        public async Task<IActionResult> AddUserProjectAsync(AddUserToProjectRequest request)
        {

            var createResponse = await Mediator.Send(request);
            if (createResponse.IsFailure)
            {
                return BadRequest(createResponse.Error);
            }
            return Ok(createResponse.Value);
        }
        /// <summary>
        /// Удалить пользователя проекта
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("removeuser")]
        public async Task<IActionResult> RemoveUserProjectAsync(RemoveUserFromProjectRequest request)
        {

            var createResponse = await Mediator.Send(request);
            if (createResponse.IsFailure)
            {
                return BadRequest(createResponse.Error);
            }
            return Ok(createResponse.Value);
        }
        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProjectAsync(UpdateProjectRequest request)
        {
            var response = await Mediator.Send(request);
            if (response.IsFailure)
            {
                return NotFound(response.Error);
            }
            return Ok(response.Value);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProjectAsync(Guid id)
        {
            var response = await Mediator.Send(new DeleteProjectRequest(id));
            if (response.IsFailure)
            {
                return NotFound(response.Error);
            }
            return Ok(response.Value);
        }
    }
}
