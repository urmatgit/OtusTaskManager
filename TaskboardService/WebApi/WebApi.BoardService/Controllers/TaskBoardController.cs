using BoardService.Domain.Entity;
using BoardService.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Repository.Abstractions;
using Services.Contract.TaskBoard;



namespace WebApi.BoardService.Controllers
{
    /// <summary>
    /// Контроллер Доска задач
    /// </summary>
    /// <param name="taskBoardRepository"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class TaskBoardController(
        ITaskBoardRepository taskBoardRepository
        ) : ControllerBase
    {   
        /// <summary>
        /// Вернуть все
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBoards()
        {
            var items = await taskBoardRepository.GetAllAsync(CancellationToken.None);
            return Ok(items);
        }

        /// <summary>
        /// Вернуть по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string GetBoard(Guid id)
        {
            return "value";
        }

        /// <summary>
        /// Добавить новую доску задач
        /// </summary>
        /// <param name="taskBoardDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskBoardDto taskBoardDto)
        {
            var taskBoard = new TaskBoard
            {
                Id = taskBoardDto.Id,
                Name = taskBoardDto.Name,
                Status = (BoardStatus)taskBoardDto.Status,
                CreateDate = DateTime.Now,
            };

            taskBoardRepository.Add(taskBoard);
            await taskBoardRepository.SaveChangesAsync();

            return Ok(taskBoardDto);
        }

        /// <summary>
        /// Обновить доску задач
        /// </summary>
        /// <param name="id"></param>
        /// <param name="taskBoardDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TaskBoardDto taskBoardDto)
        {
            var taskBoard = await taskBoardRepository.GetAsync(id, CancellationToken.None);

            if (taskBoard == null) 
            {
                return BadRequest();
            }
            taskBoard.Name = taskBoardDto.Name;
            taskBoard.Status = (BoardStatus) taskBoardDto.Status;
            
            taskBoardRepository.Update(taskBoard);
            await taskBoardRepository.SaveChangesAsync(CancellationToken.None);

            return Ok(taskBoard);
        }

        /// <summary>
        /// Удалить доску задач
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            taskBoardRepository.Delete(id);
            await taskBoardRepository.SaveChangesAsync(CancellationToken.None);

            return Ok();
        }
    }
}
