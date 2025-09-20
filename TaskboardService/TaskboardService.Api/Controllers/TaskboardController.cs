using Microsoft.AspNetCore.Mvc;

using TaskboardService.Api.Models;
using TaskboardService.Business.Services.Abstract;
using TaskboardService.DataAccess.Entity;
using TaskboardService.DataAccess.Enums;

namespace TaskboardService.Api.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class TaskboardController(ITaskboardService taskboardService) : ControllerBase
    {
        private readonly ITaskboardService _taskboardService = taskboardService;

        [HttpGet("List/{projectId:guid}")]
        public async Task<IActionResult> GetTaskboards(Guid projectId) 
        {
            var list = await _taskboardService.GetTaskboardListAsync(projectId);

            var models = list.Select(t => t.ToDto()).OrderBy(t => t.SortOrder);

            return Ok(models);
        }

        [HttpPost("Item")]
        public async Task<IActionResult> CreateTaskboard([FromBody()] TaskboardDto taskboardDto) 
        {
            var taskboard = new Taskboard() 
            {
                Id = Guid.NewGuid(),
                ProjectId = taskboardDto.ProjectId             
            };
            taskboardDto.ToEntity(taskboard);

            var result = await _taskboardService.CreateTaskboardAsync(taskboard);

            taskboardDto.Id = result.Id;

            return Ok(taskboardDto);
        }

        [HttpPut("Item/{taskboardId:guid}")]
        public async Task<IActionResult> UpdateTaskboard(Guid taskboardId, [FromBody] TaskboardDto taskboardDto) 
        {
            var entity = await _taskboardService.GetTaskboardAsync(taskboardId);
            taskboardDto.ToEntity(entity);
            await _taskboardService.UpdateTaskboardAsync(entity);

            return Ok(taskboardDto);
        }

        [HttpGet("ChangeStatus/{taskboardId:guid}/{newStatus:int}")]
        public async Task<IActionResult> ChangeTaskboardStatus(Guid taskboardId, TaskboardStatus newStatus) 
        {
            var entity = await _taskboardService.GetTaskboardAsync(taskboardId);
            entity.Status = newStatus;
            await _taskboardService.UpdateTaskboardAsync(entity);

            return Ok(entity.ToDto());
        }

        [HttpDelete("Item/{taskboardId:guid}")]
        public async Task<IActionResult> DeleteTaskboard(Guid taskboardId) 
        {
            var result = await _taskboardService.DeleteTaskboardAsync(taskboardId);
            return Ok(result);
        }
    }

}