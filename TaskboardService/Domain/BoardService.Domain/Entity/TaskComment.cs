using BoardService.Domain.Abstraction;

namespace BoardService.Domain.Entity
{
    /// <summary>
    /// Комментарий задачи
    /// </summary>
    public class TaskComment : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Имя автора
        /// </summary>
        public required string Author { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public required DateTime CreateDate { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Задача
        /// </summary>
        public TaskItem Tasktem { get; set; } = null!;

    }
}
