using AutoMapper;
using BoardService.Domain.Entity;
using Repository.Abstractions;
using Services.Abstractions;
using Services.Contract.CheckList;

namespace Services.Implementation
{
    /// <summary>
    /// Сервис чек-листа
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="checkListRepository"></param>
    public class CheckListService(
        IMapper mapper,
        ICheckListRepository checkListRepository
        ) : ICheckListService
    {
        /// <summary>
        /// Получить чек-лист
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>CheckListDto</returns>
        public async Task<CheckListDto?> GetByIdAsync(Guid id)
        {
            var checkList = await checkListRepository.GetAsync(id, CancellationToken.None);
            return checkList == null ? null : mapper.Map<CheckList, CheckListDto>(checkList);
        }
    }
}
