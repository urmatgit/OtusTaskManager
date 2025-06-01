using Services.Contract.TaskComment;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса Комментарий задачи
    /// </summary>
    public interface ITaskCommentService
    {
        /// <summary>
        /// Получить Комментарий задачи
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        Task<TaskCommentDto?> GetByIdAsync(Guid id);
    }
}
