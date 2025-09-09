using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using TaskboardService.DataAccess.Abstraction;
using TaskboardService.DataAccess.Enums;

namespace TaskboardService.DataAccess.Entity
{
    /// <summary>
    /// Доска задач
    /// </summary>
    public class Taskboard : BaseEntity
    {
        /// <summary>
        /// Идентификатор проекта, которому привязана доска задач.
        /// </summary>
        [BsonElement("projectId")]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid ProjectId { get; set; }
        /// <summary>
        /// Порядок сортировки досок в списке.
        /// </summary>
        [BsonElement("sortOrder")]
        public float SortOrder { get; set; }

        /// <summary>
        /// Название доски
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Статус
        /// </summary>
        [BsonElement("status")]
        public TaskboardStatus Status { get; set; }

        /// <summary>
        /// Список колонок доски
        /// </summary>
        [BsonElement("columns")]
        public List<TaskboardColumn> Columns { get; set; } = [];
    }
}