using BoardService.Domain.Entity;
using BoardService.Domain.Enums;
using BoardService.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Repository.Abstractions;
using Services.Contract.TaskComment;
using WebApi.BoardService.Mapper;
using WebApi.BoardService.Model;

namespace WebApi.BoardService.Controllers
{
    /// <summary>
    /// Контроллер Комментарий задачи
    /// </summary>
    /// <param name="taskCommentRepository"></param>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskCommentController(
        ITaskCommentRepository taskCommentRepository
        ) : Controller
    {
        /// <summary>
        /// Вернуть все комментарии
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet("lst/{id:guid}")]
        [HttpGet("lst/{id:guid}/{page:int:min(1)}")]
        [HttpGet("lst/{id:guid}/{page:int:min(1)}/{count:int:min(1)}")]
        public async Task<IActionResult> GetTaskComments(Guid id, int? page, int? count)
        {
            var res = new TaskCommentResponse
            {
                Filter = PageFilter.Default(page, count)
            };
            res.Data = [.. (
                from x in await taskCommentRepository.GetFilteredAsync(id, res.Filter)
                select TaskCommentMapper.MapFromModel(x)
                )];

            return Ok(res);
        }

        /// <summary>
        /// Вернуть комментарий по ИД
        /// </summary>
        /// <param name="id">Идентификатор комментария</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTaskComment(Guid id)
        {
            var item = await taskCommentRepository.GetAsync(id, CancellationToken.None);

            if (item == null)
            {
                return BadRequest("Комментарий не найден");
            }

            return Ok(TaskCommentMapper.MapFromModel(item));
        }

        /// <summary>
        /// Добавить новый комментарий
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="taskCommentDto"></param>
        /// <returns></returns>
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> AddTaskComment(Guid id, [FromBody] TaskCommentAddDto taskCommentDto)
        {
            var taskComment = new TaskComment
            {                
                Id = taskCommentDto.Id,
                Author = taskCommentDto.Author,
                CreateDate = DateTime.Now,
                Text = taskCommentDto.Text,
                TaskId = id
            };

            taskCommentRepository.Add(taskComment);
            await taskCommentRepository.SaveChangesAsync();

            return Ok(TaskCommentMapper.MapFromModel(taskComment));
        }

        /// <summary>
        /// Обновить комментарий
        /// </summary>
        /// <param name="id">Идентификатор комментария</param>
        /// <param name="taskCommentDto"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTaskComment(Guid id, [FromBody] TaskCommentUpdateDto taskCommentDto)
        {
            var taskItem = await taskCommentRepository.GetAsync(id, CancellationToken.None);

            if (taskItem == null)
            {
                return BadRequest();
            }
            taskItem.Author = taskCommentDto.Author ?? taskItem.Author;
            taskItem.Text = taskCommentDto.Text ?? taskItem.Text;                        

            taskCommentRepository.Update(taskItem);
            await taskCommentRepository.SaveChangesAsync(CancellationToken.None);

            return Ok(TaskCommentMapper.MapFromModel(taskItem));
        }

        /// <summary>
        /// Удалить комментарий
        /// </summary>
        /// <param name="id">Идентификатор комментария</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaskComment(Guid id)
        {
            taskCommentRepository.Delete(id);
            await taskCommentRepository.SaveChangesAsync(CancellationToken.None);

            return Ok();
        }

    }
}
