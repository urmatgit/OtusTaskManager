using Services.Contract.BorderColumn;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс колонки доски задач
    /// </summary>
    public interface IBoardColumnService
    {
        /// <summary>
        /// Получить колонку доски задач
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        Task<BoardColumnDto?> GetByIdAsync(Guid id);
    }
}
