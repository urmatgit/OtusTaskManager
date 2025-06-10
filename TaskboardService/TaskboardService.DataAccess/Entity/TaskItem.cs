using TaskboardService.DataAccess.Abstraction;
using TaskboardService.DataAccess.Enums;

namespace TaskboardService.DataAccess.Entity
{
    /// <summary>
    /// Задача
    /// </summary>
    public class TaskItem : BaseEntity
    {
        /// <summary>
        /// Порядок сортировки задачи в колонке.
        /// </summary>
        public float SortOrder { get; set; }

        /// <summary>
        /// Наименование задачи
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// описание задачи
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Дата создание задачи
        /// </summary>
        public required DateTime CreatedDate { get; set; }

        /// <summary>
        /// Срок выполнения задачи
        /// </summary>
        public required DateTime ExecutionDate { get; set; }

        /// <summary>
        /// Фактический срок выполнения задачи
        /// </summary>
        public DateTime? FactExecutionDate { get; set; }

        /// <summary>
        /// Приоритет задачи
        /// </summary>
        public TaskPriority Priority { get; set; }

        /// <summary>
        /// Список исполнителей задачи
        /// </summary>
        public List<TaskboardUser> Executors { get; set; } = [];

        /// <summary>
        /// Список комментариев задачи
        /// </summary>
        public List<TaskComment> Comments { get; set; } = [];

        /// <summary>
        /// Список файлов задачи
        /// </summary>
        public List<TaskFile> Files { get; set; } = [];

        /// <summary>
        /// список чек-листов
        /// </summary>
        public List<CheckList> CheckLists { get; set; } = [];
    }
}
