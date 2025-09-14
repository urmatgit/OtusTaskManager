using BoardService.Domain.Entity;
using BoardService.Domain.Enums;
using BoardService.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Repository.Abstractions;
using Services.Contract.TaskItem;
using WebApi.BoardService.Mapper;
using WebApi.BoardService.Model;

namespace WebApi.BoardService.Controllers
{
    /// <summary>
    /// Контроллер Задача
    /// </summary>
    /// <param name="taskItemRepository"></param>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskItemController(
        ITaskItemRepository taskItemRepository
        ) : Controller
    {
        /// <summary>
        /// Вернуть все задачи колонки
        /// </summary>
        /// <param name="id">Идентификатор колонки</param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet("lst/{id:guid}")]
        [HttpGet("lst/{id:guid}/{page:int:min(1)}")]
        [HttpGet("lst/{id:guid}/{page:int:min(1)}/{count:int:min(1)}")]
        public async Task<IActionResult> GetTaskItems(Guid id, int? page, int? count)
        {
            var res = new TaskItemResponse
            {
                Filter = PageFilter.Default(page, count)
            };
            res.Data = [.. (
                from x in await taskItemRepository.GetFilteredAsync(id, res.Filter)
                select TaskItemMapper.MapFromModel(x)
                )];

            return Ok(res);
        }

        /// <summary>
        /// Вернуть задачу по ИД
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTaskItem(Guid id)
        {
            var item = await taskItemRepository.GetAsync(id, CancellationToken.None);

            if (item == null)
            {
                return BadRequest("Доска задач не найдена");
            }

            return Ok(TaskItemMapper.MapFromModel(item));
        }

        /// <summary>
        /// Добавить новую задачу
        /// </summary>
        /// <param name="id">Идентификатор колонки</param>
        /// <param name="taskItemDto"></param>
        /// <returns></returns>
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> AddTaskItem(Guid id, [FromBody] TaskItemAddDto taskItemDto)
        {
            var taskItem = new TaskItem
            {
                BoardColumnId = id,
                CreateDate = DateTime.Now,
                Description = taskItemDto.Description,
                FactDate = taskItemDto.FactDate,
                Id = taskItemDto.Id,
                Name = taskItemDto.Name,
                PlanDate = taskItemDto.PlanDate,
                Priority = (TaskPriority)taskItemDto.Priority,                
            };

            taskItemRepository.Add(taskItem);
            await taskItemRepository.SaveChangesAsync();

            return Ok(TaskItemMapper.MapFromModel(taskItem));
        }

        /// <summary>
        /// Обновить задачу
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="taskItemDto"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTaskItem(Guid id, [FromBody] TaskItemUpdateDto taskItemDto)
        {
            var taskItem = await taskItemRepository.GetAsync(id, CancellationToken.None);

            if (taskItem == null)
            {
                return BadRequest();
            }
            taskItem.Name = taskItemDto.Name ?? taskItem.Name;
            taskItem.Description = taskItemDto.Description ?? taskItem.Description;
            taskItem.FactDate = taskItemDto.FactDate ?? taskItem.FactDate;
            taskItem.PlanDate = taskItemDto.PlanDate ?? taskItem.PlanDate;
            if (taskItemDto.Priority != null)
                taskItem.Priority = (TaskPriority)taskItemDto.Priority;

            taskItemRepository.Update(taskItem);
            await taskItemRepository.SaveChangesAsync(CancellationToken.None);

            return Ok(TaskItemMapper.MapFromModel(taskItem));
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaskItem(Guid id)
        {
            taskItemRepository.Delete(id);
            await taskItemRepository.SaveChangesAsync(CancellationToken.None);

            return Ok();
        }

    }
}
