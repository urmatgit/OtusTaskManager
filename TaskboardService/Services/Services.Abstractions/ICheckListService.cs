using Services.Contract.CheckList;

namespace Services.Abstractions
{
    /// <summary>
    /// Чек-лист
    /// </summary>
    public interface ICheckListService
    {
        /// <summary>
        /// Получить чек-лист
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        Task<CheckListDto?> GetByIdAsync(Guid id);
    }
}
