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
        /// Вип-лимит. Определяет количество задач, которое может быть определено в колонке.
        /// </summary>
        [BsonElement("vipLimit")]
        public int VipLimit { get; set; }

        /// <summary>
        /// Список задач в колонке.
        /// </summary>
        [BsonElement("items")]
        public List<TaskItem> Items { get; set; } = [];
    }
}
