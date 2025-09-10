using MongoDB.Bson.Serialization.Attributes;

using TaskboardService.DataAccess.Abstraction;

namespace TaskboardService.DataAccess.Entity
{
    public class Project : BaseEntity
    {
        /// <summary>
        /// Доски текущего проекта.
        /// </summary>
        [BsonElement("taskboards")]
        public List<Taskboard> Taskboards { get; set; } = [];
    }
}