using Services.Contract.TaskItem;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса Задача
    /// </summary>
    public interface ITaskItemService
    {
        /// <summary>
        /// Получить Задачу
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        Task<TaskItemDto?> GetByIdAsync(Guid id);
    }
}
