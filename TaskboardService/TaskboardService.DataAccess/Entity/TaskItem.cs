using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
        /// Идентификатор доски задач.
        /// </summary>
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid TaskboardId { get; set; }

        /// <summary>
        /// Идентификатор столбца задачи.
        /// </summary>
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid TaskboardColumnId { get; set; }

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
        /// Постановщик задачи.
        /// </summary>
        public TaskboardUser Author { get; set; }

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
        public List<TaskAttachment> Files { get; set; } = [];

        /// <summary>
        /// список чек-листов
        /// </summary>
        public List<CheckList> CheckLists { get; set; } = [];
    }
}
