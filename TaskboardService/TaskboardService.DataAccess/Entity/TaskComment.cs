using MongoDB.Bson.Serialization.Attributes;

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
        [BsonElement("author")]
        public TaskboardUser Author { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        [BsonElement("text")]
        public string? Text { get; set; }

        /// <summary>
        /// Список файлов в комментарии
        /// </summary>
        [BsonElement("commentAttachments")]
        public List<TaskAttachment> CommentAttachments { get; set; } = [];
    }
}
