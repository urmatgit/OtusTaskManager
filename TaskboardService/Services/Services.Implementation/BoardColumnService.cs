using AutoMapper;
using BoardService.Domain.Entity;
using Repository.Abstractions;
using Services.Abstractions;
using Services.Contract.BorderColumn;

namespace Services.Implementation
{
    /// <summary>
    /// Сервис колонки доски задач
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="boardColumnRepository"></param>
    public class BoardColumnService(
        IMapper mapper,
        IBoardColumnRepository boardColumnRepository
        ) : IBoardColumnService
    {
        /// <summary>
        /// Получить колонку доски задач
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>BoardColumnDto</returns>
        public async Task<BoardColumnDto?> GetByIdAsync(Guid id)
        {
            var course = await boardColumnRepository.GetAsync(id, CancellationToken.None);
            return course == null ? null : mapper.Map<BoardColumn, BoardColumnDto>(course);
        }
    }
}
