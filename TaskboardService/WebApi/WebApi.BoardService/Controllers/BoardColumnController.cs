using BoardService.Domain.Entity;
using BoardService.Domain.Enums;
using BoardService.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Repository.Abstractions;
using Services.Contract.BorderColumn;
using WebApi.BoardService.Mapper;
using WebApi.BoardService.Model;

namespace WebApi.BoardService.Controllers
{
    /// <summary>
    /// Контроллер Колонка
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BoardColumnController(
        IBoardColumnRepository boardColumnRepository
        ) : Controller
    {
        /// <summary>
        /// Вернуть все колонки
        /// </summary>
        /// <param name="id">Идентификатор доски задач</param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet("lst/{id:guid}")]
        [HttpGet("lst/{id:guid}/{page:int:min(1)}")]
        [HttpGet("lst/{id:guid}/{page:int:min(1)}/{count:int:min(1)}")]
        public async Task<IActionResult> GetBoardColumns(Guid id, int? page, int? count)
        {
            var res = new BoardColumnResponse
            {
                Filter = PageFilter.Default(page, count)
            };
            res.Data = [.. (
                from x in await boardColumnRepository.GetFilteredAsync(id, res.Filter)
                select BoardColumnMapper.MapFromModel(x)
                )];

            return Ok(res);
        }

        /// <summary>
        /// Вернуть колонку по ИД
        /// </summary>
        /// <param name="id">Идентификатор колонки</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBoardColumn(Guid id)
        {
            var item = await boardColumnRepository.GetAsync(id, CancellationToken.None);

            if (item == null)
            {
                return BadRequest("Доска задач не найдена");
            }

            return Ok(BoardColumnMapper.MapFromModel(item));            
        }

        /// <summary>
        /// Добавить новую колонку
        /// </summary>
        /// <param name="id">Идентификатор доски задач</param>
        /// <param name="boardColumnDto"></param>
        /// <returns></returns>
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> AddBoardColumn(Guid id, [FromBody] BoardColumnAddDto boardColumnDto)
        {
            var boardColumn = new BoardColumn
            {
                Id = boardColumnDto.Id,
                Name = boardColumnDto.Name,                
                CreateDate = DateTime.Now,
                VipLimit = boardColumnDto.VipLimit,
                ColumnType = (ColumnType)boardColumnDto.ColumnType,
                TaskBoardId = id
            };

            boardColumnRepository.Add(boardColumn);
            await boardColumnRepository.SaveChangesAsync();

            return Ok(BoardColumnMapper.MapFromModel(boardColumn));            
        }

        /// <summary>
        /// Обновить колонку
        /// </summary>
        /// <param name="id">Идентификатор колонки</param>
        /// <param name="boardColumnDto"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBoardColumn(Guid id, [FromBody] BoardColumnUpdateDto boardColumnDto)
        {
            var boardColumn = await boardColumnRepository.GetAsync(id, CancellationToken.None);

            if (boardColumn == null)
            {
                return BadRequest();
            }
            boardColumn.Name = boardColumnDto.Name ?? boardColumn.Name;
            boardColumn.VipLimit = boardColumnDto.VipLimit ?? boardColumn.VipLimit;
            if (boardColumnDto.ColumnType != null)
                boardColumn.ColumnType = (ColumnType)boardColumnDto.ColumnType;

            boardColumnRepository.Update(boardColumn);
            await boardColumnRepository.SaveChangesAsync(CancellationToken.None);

            return Ok(BoardColumnMapper.MapFromModel(boardColumn));            
        }

        /// <summary>
        /// Удалить колонку
        /// </summary>
        /// <param name="id">Идентификатор колонки</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBoardColumn(Guid id)
        {
            boardColumnRepository.Delete(id);
            await boardColumnRepository.SaveChangesAsync(CancellationToken.None);

            return Ok();
        }        
    }
}
