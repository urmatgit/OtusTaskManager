using MongoDB.Bson.Serialization.Attributes;

using TaskboardService.DataAccess.Abstraction;

namespace TaskboardService.DataAccess.Entity
{
    /// <summary>
    /// Колонка доски задач.
    /// </summary>
    public class TaskboardColumn : BaseEntity
    {
        /// <summary>
        /// Идентификатор доски задач.
        /// </summary>
        [BsonElement("taskboardId")]
        public Guid TaskboardId { get; set; }

        /// <summary>
        /// Порядок сортировки.
        /// </summary>
        [BsonElement("sortOrder")]
        public float SortOrder { get; set; }

        /// <summary>
        /// Название колонки.
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Цвет заголовка колонки.
        /// </summary>
        [BsonElement("color")]
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// Вип-лимит. Определяет количество задач, которое может быть определено в колонке.
        /// </summary>
        [BsonElement("wipLimit")]
        public int WipLimit { get; set; }

        /// <summary>
        /// Список задач в колонке.
        /// </summary>
        [BsonElement("tasks")]
        public List<TaskItem> Tasks { get; set; } = [];
    }
}
