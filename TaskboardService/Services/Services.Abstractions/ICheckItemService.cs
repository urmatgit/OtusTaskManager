using Services.Contract.CheckItem;

namespace Services.Abstractions
{
    /// <summary>
    /// Сервис элементов чек-листа
    /// </summary>
    public interface ICheckItemService
    {
        /// <summary>
        /// Получить элементы чек-листа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CheckItemDto?> GetByIdAsync(Guid id);
    }
}
