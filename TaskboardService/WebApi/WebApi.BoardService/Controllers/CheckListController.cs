using BoardService.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Abstractions;
using Services.Contract.CheckList;
using WebApi.BoardService.Mapper;
using WebApi.BoardService.Model;

namespace WebApi.BoardService.Controllers
{
    /// <summary>
    /// Контроллер Чек-листа
    /// </summary>
    /// <param name="checkListRepository"></param>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CheckListController(
        ICheckListRepository checkListRepository
        ) : Controller
    {
        /// <summary>
        /// Вернуть все чек-листы
        /// </summary>
        /// <param name="id">Идентификатор колонки</param>        
        /// <returns></returns>
        [HttpGet("lst/{id:guid}")]        
        public async Task<IActionResult> GetTaskItems(Guid id)
        {
            var res = new CheckListResponse
            {
                Data = [.. (
                from x in await checkListRepository.GetChecksAsync(id)
                select CheckListMapper.MapFromModel(x)
                )]
            };            

            return Ok(res);
        }

        /// <summary>
        /// Вернуть чек-лист по ИД
        /// </summary>
        /// <param name="id">Идентификатор чек-листа</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTaskItem(Guid id)
        {
            var item = await checkListRepository.GetAsync(id, CancellationToken.None);

            if (item == null)
            {
                return BadRequest("Доска задач не найдена");
            }

            return Ok(CheckListMapper.MapFromModel(item));
        }

        /// <summary>
        /// Добавить чек-лист
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="checkListDto"></param>
        /// <returns></returns>
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> AddTaskItem(Guid id, [FromBody] CheckListAddDto checkListDto)
        {
            var checkList = new CheckList
            {                
                Id = checkListDto.Id,
                Name = checkListDto.Name,
                TaskId = id
            };

            checkListRepository.Add(checkList);
            await checkListRepository.SaveChangesAsync();

            return Ok(CheckListMapper.MapFromModel(checkList));
        }

        /// <summary>
        /// Обновить чек-лист
        /// </summary>
        /// <param name="id">Идентификатор чек-листа</param>
        /// <param name="checkListDto"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTaskItem(Guid id, [FromBody] CheckListUpdateDto checkListDto)
        {
            var checkList = await checkListRepository.GetAsync(id, CancellationToken.None);

            if (checkList == null)
            {
                return BadRequest();
            }
            checkList.Name = checkListDto.Name ?? checkList.Name;            

            checkListRepository.Update(checkList);
            await checkListRepository.SaveChangesAsync(CancellationToken.None);

            return Ok(CheckListMapper.MapFromModel(checkList));
        }

        /// <summary>
        /// Удалить чек-лист
        /// </summary>
        /// <param name="id">Идентификатор чек-листа</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaskItem(Guid id)
        {
            checkListRepository.Delete(id);
            await checkListRepository.SaveChangesAsync(CancellationToken.None);

            return Ok();
        }

    }
}
