using TaskboardService.DataAccess.Abstraction;

namespace TaskboardService.DataAccess.Entity
{
    /// <summary>
    /// Колонка доски задач
    /// </summary>
    public class TaskboardColumn : BaseEntity
    {
        /// <summary>
        /// Порядок сортировки
        /// </summary>
        public float SortOrder { get; set; }

        /// <summary>
        /// Название колонки
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Дата создание колонки
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Вип-лимит. Определяет количество задач, которое может быть определено в колонке.
        /// </summary>
        public int VipLimit { get; set; }

        /// <summary>
        /// Список задач
        /// </summary>
        public List<TaskItem> TaskItems { get; set; } = [];
    }
}
