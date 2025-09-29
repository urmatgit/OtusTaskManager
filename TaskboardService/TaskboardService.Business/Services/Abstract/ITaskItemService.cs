using TaskboardService.DataAccess.Entity;

namespace TaskboardService.Business.Services.Abstract
{
    /// <summary>
    /// Интерфейс сервиса работы с задачами.
    /// </summary>
    public interface ITaskItemService
    {
        /// <summary>
        /// Получение сведений конкретной задачи.
        /// </summary>
        /// <param name="taskItemId">идентификатор задачи</param>
        /// <returns></returns>
        Task<TaskItem> GetTaskItemAsync(Guid taskItemId);
        /// <summary>
        /// Создание новой задачи.
        /// </summary>
        /// <param name="taskItem">создаваемая задача</param>
        /// <returns></returns>
        Task<TaskItem> CreateTaskItemAsync(TaskItem taskItem);
        /// <summary>
        /// Обновление существующей задачи.
        /// </summary>
        /// <param name="taskItemId">идентификатор обновляемой задачи</param>
        /// <param name="taskItem">обновляемая задача</param>
        /// <returns></returns>
        Task<TaskItem?> UpdateTaskItemAsync(TaskItem taskItem);
        /// <summary>
        /// Удаление существующей задачи.
        /// </summary>
        /// <param name="id">идентификатор задачи</param>
        /// <returns></returns>
        Task<bool> DeleteTaskItemAsync(Guid taskboardId, Guid columnId, Guid id);
    }
}