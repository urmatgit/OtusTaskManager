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
        [BsonElement("taskboardId")]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid TaskboardId { get; set; }

        /// <summary>
        /// Идентификатор столбца задачи.
        /// </summary>
        [BsonElement("taskboardColumnId")]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid TaskboardColumnId { get; set; }

        /// <summary>
        /// Порядок сортировки задачи в колонке.
        /// </summary>
        [BsonElement("sortOrder")]
        public float SortOrder { get; set; }

        /// <summary>
        /// Наименование задачи
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// описание задачи
        /// </summary>
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Срок выполнения задачи
        /// </summary>
        [BsonElement("executionDate")]
        public DateTime? ExecutionDate { get; set; }

        /// <summary>
        /// Фактический срок выполнения задачи
        /// </summary>
        [BsonElement("factExecutionDate")]
        public DateTime? FactExecutionDate { get; set; }

        /// <summary>
        /// Приоритет задачи
        /// </summary>
        [BsonElement("priority")]
        public TaskPriority Priority { get; set; }

        /// <summary>
        /// Прогресс выполнения задачи.
        /// </summary>
        [BsonElement("progress")]
        public int Progress { get; set; }

        /// <summary>
        /// Цвет заголовка задачи.
        /// </summary>
        [BsonElement("headerColor")]
        public string HeaderColor { get; set; } = string.Empty;

        /// <summary>
        /// Постановщик задачи.
        /// </summary>
        [BsonElement("author")]
        public TaskboardUser Author { get; set; }

        /// <summary>
        /// Список исполнителей задачи
        /// </summary>
        [BsonElement("executors")]
        public List<TaskboardUser> Executors { get; set; } = [];

        /// <summary>
        /// Список комментариев задачи
        /// </summary>
        [BsonElement("comments")]
        public List<TaskComment> Comments { get; set; } = [];

        /// <summary>
        /// Список файлов задачи
        /// </summary>
        [BsonElement("attachments")]
        public List<TaskAttachment> Attachments { get; set; } = [];

        /// <summary>
        /// список чек-листов
        /// </summary>
        [BsonElement("checkLists")]
        public List<CheckList> CheckLists { get; set; } = [];
    }
}
