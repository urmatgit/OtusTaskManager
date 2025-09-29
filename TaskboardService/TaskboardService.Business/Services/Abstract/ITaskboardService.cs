using TaskboardService.DataAccess.Entity;

namespace TaskboardService.Business.Services.Abstract
{
    public interface ITaskboardService
    {
        /// <summary>
        /// Получение списка досок по идентификатору проекта.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<List<Taskboard>> GetTaskboardListAsync(Guid projectId);
        /// <summary>
        /// Чтение данных по конкретной доске задач.
        /// </summary>
        /// <param name="taskboardId"></param>
        /// <returns></returns>
        Task<Taskboard> GetTaskboardAsync(Guid taskboardId);
        /// <summary>
        /// Создание новой доски задач.
        /// </summary>
        /// <param name="taskboard"></param>
        /// <returns></returns>
        Task<Taskboard> CreateTaskboardAsync(Taskboard taskboard);
        /// <summary>
        /// Обновление существующей доски задач.
        /// </summary>
        /// <param name="taskboard"></param>
        /// <returns></returns>
        Task<Taskboard?> UpdateTaskboardAsync(Taskboard taskboard);
        /// <summary>
        /// Удаление доски задач по идентификатору.
        /// </summary>
        /// <param name="taskboardId"></param>
        /// <returns></returns>
        Task<bool> DeleteTaskboardAsync(Guid taskboardId);
        /// <summary>
        /// Добавление нового столбца в доску задач.
        /// </summary>
        /// <param name="taskboardId"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        Task<Taskboard> AddColumnAsync(Guid taskboardId, TaskboardColumn column);
        /// <summary>
        /// Обновление существующего столбца в доске задач.
        /// </summary>
        /// <param name="taskboardId"></param>
        /// <param name="columnId"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        Task<Taskboard> UpdateColumnAsync(Guid taskboardId, Guid columnId, TaskboardColumn column);
        /// <summary>
        /// Удаление столбца из доски задач.
        /// </summary>
        /// <param name="taskboardId"></param>
        /// <param name="columnId"></param>
        /// <returns></returns>
        Task<Taskboard> DeleteColumnAsync(Guid taskboardId, Guid columnId);
    }
}