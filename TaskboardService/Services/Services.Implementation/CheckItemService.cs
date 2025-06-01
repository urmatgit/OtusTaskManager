using AutoMapper;
using BoardService.Domain.Entity;
using Repository.Abstractions;
using Services.Abstractions;
using Services.Contract.CheckItem;

namespace Services.Implementation
{
    /// <summary>
    /// Сервис элементов чек-листа
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="boardColumnRepository"></param>
    public class CheckItemService (
        IMapper mapper,
        ICheckItemRepository checkItemRepository
        ) : ICheckItemService
    {
        /// <summary>
        /// Получить элементы чек-листа
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>CheckItemDto</returns>
        public async Task<CheckItemDto?> GetByIdAsync(Guid id)
        {
            var checkItem = await checkItemRepository.GetAsync(id, CancellationToken.None);
            return checkItem == null ? null : mapper.Map<CheckItem, CheckItemDto>(checkItem);
        }
    }
}
