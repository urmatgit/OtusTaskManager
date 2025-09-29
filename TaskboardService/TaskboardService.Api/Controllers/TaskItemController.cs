using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TaskboardService.Api.Models;
using TaskboardService.Business.Services.Abstract;
using TaskboardService.DataAccess.Entity;

namespace TaskboardService.Api.Controllers
{
    /// <summary>
    /// Контроллер по работе с задачами
    /// </summary>
    /// <param name="taskItemService"></param>
    [ApiController()]
    [Route("api/[controller]")]
    public class TaskItemController(ITaskItemService taskItemService) : ControllerBase
    {
        private readonly ITaskItemService _taskItemService = taskItemService;

        /// <summary>
        /// Создание новой задачи
        /// </summary>
        /// <param name="taskItemDto"></param>
        /// <returns></returns>
        [HttpPost("Task")]
        public async Task<IActionResult> CreateTaskItem([FromBody()] TaskItemDto taskItemDto) 
        {
            var entity = new TaskItem()
            {
                Id = Guid.NewGuid(),
                TaskboardId = taskItemDto.TaskboardId,
                TaskboardColumnId = taskItemDto.TaskboardColumnId
            };
            taskItemDto.ToEntity(entity);

            try
            {
                await _taskItemService.CreateTaskItemAsync(entity);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

            taskItemDto.Id = entity.Id;

            return Ok(taskItemDto);
        }

        /// <summary>
        /// Обновление существующей задачи.
        /// </summary>
        /// <param name="taskItemDto"></param>
        /// <returns></returns>
        [HttpPut("Task")]
        public async Task<IActionResult> UpdateTaskItem([FromBody()] TaskItemDto taskItemDto) 
        {
            var entity = new TaskItem()
            {
                Id = taskItemDto.Id,
                TaskboardId = taskItemDto.TaskboardId,
                TaskboardColumnId = taskItemDto.TaskboardColumnId
            };
            taskItemDto.ToEntity(entity);

            await _taskItemService.UpdateTaskItemAsync(entity);

            return Ok(taskItemDto);
        }
    }
}