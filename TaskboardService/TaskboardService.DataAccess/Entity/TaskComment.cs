using TaskboardService.DataAccess.Abstraction;

namespace TaskboardService.DataAccess.Entity
{
    /// <summary>
    /// Комментарий задачи
    /// </summary>
    public class TaskComment : BaseEntity
    {
        /// <summary>
        /// Автор комментария
        /// </summary>
        public TaskboardUser Author { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Список файлов в комментарии
        /// </summary>
        public List<TaskFile> CommentFiles { get; set; } = [];
    }
}
