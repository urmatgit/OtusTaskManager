using Services.Contract.TaskBoard;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс доски задач
    /// </summary>
    public interface ITaskBoardService
    {
        /// <summary>
        /// Получить доску задач
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        Task<TaskBoardDto?> GetByIdAsync(Guid id);
    }
}
