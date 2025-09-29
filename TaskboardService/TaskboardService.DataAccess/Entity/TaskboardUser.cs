using MongoDB.Bson.Serialization.Attributes;

using TaskboardService.DataAccess.Abstraction;

namespace TaskboardService.DataAccess.Entity
{
    public class TaskboardUser : BaseEntity
    {        
        /// <summary>
        /// Полное имя пользователя
        /// </summary>
        [BsonElement("fullName")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Электронная почта
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;
    }
}