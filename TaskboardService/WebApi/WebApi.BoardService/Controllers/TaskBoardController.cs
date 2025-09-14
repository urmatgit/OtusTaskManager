using BoardService.Domain.Entity;
using BoardService.Domain.Enums;
using BoardService.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Repository.Abstractions;
using Services.Contract.TaskBoard;
using WebApi.BoardService.Model;
using WebApi.BoardService.Mapper;


namespace WebApi.BoardService.Controllers
{
    /// <summary>
    /// Контроллер Доска задач
    /// </summary>
    /// <param name="taskBoardRepository"></param>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskBoardController(
        ITaskBoardRepository taskBoardRepository
        ) : ControllerBase
    {   
        /// <summary>
        /// Вернуть все джоски задач
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("{page:int:min(1)}")]
        [HttpGet("{page:int:min(1)}/{count:int:min(1)}")]
        public async Task<IActionResult> GetBoards(int? page, int? count)
        {
            var res = new TaskBoardResponse
            {
                Filter = PageFilter.Default(page, count)
            };
            res.Data = [.. (
                from x in await taskBoardRepository.GetFilteredAsync(res.Filter)
                select TaskBoardMapper.MapFromModel(x)
                )];                

            return Ok(res);
        }

        /// <summary>
        /// Вернуть доску задач по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBoard(Guid id)
        {
            var item = await taskBoardRepository.GetAsync(id, CancellationToken.None);

            if (item == null)
            {
                return BadRequest("Доска задач не найдена");
            }

            return Ok(TaskBoardMapper.MapFromModel(item));
        }

        /// <summary>
        /// Добавить новую доску задач
        /// </summary>
        /// <param name="taskBoardDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskBoardAddDto taskBoardDto)
        {
            var taskBoard = new TaskBoard
            {
                Id = taskBoardDto.Id,
                Name = taskBoardDto.Name,
                Status = BoardStatus.AtWork,
                CreateDate = DateTime.Now,
            };

            taskBoardRepository.Add(taskBoard);
            await taskBoardRepository.SaveChangesAsync();

            return Ok(TaskBoardMapper.MapFromModel(taskBoard));
        }

        /// <summary>
        /// Обновить доску задач
        /// </summary>
        /// <param name="id"></param>
        /// <param name="taskBoardDto"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTaskBoard(Guid id, [FromBody] TaskBoardUpdateDto taskBoardDto)
        {
            var taskBoard = await taskBoardRepository.GetAsync(id, CancellationToken.None);

            if (taskBoard == null) 
            {
                return BadRequest();
            }
            taskBoard.Name = taskBoardDto.Name ?? taskBoard.Name;
            if (taskBoardDto.Status != null)
                taskBoard.Status = (BoardStatus)taskBoardDto.Status;
            
            taskBoardRepository.Update(taskBoard);
            await taskBoardRepository.SaveChangesAsync(CancellationToken.None);

            return Ok(TaskBoardMapper.MapFromModel(taskBoard));
        }

        /// <summary>
        /// Удалить доску задач
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaskBoard(Guid id)
        {
            taskBoardRepository.Delete(id);
            await taskBoardRepository.SaveChangesAsync(CancellationToken.None);

            return Ok();
        }
    }
}
