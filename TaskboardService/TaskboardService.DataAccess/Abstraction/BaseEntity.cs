using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskboardService.DataAccess.Abstraction
{
    /// <summary>
    /// Базовый класс сущностей.
    /// </summary>
    public abstract class BaseEntity : IBaseEntity<Guid>
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        [BsonElement("id")]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время создания сущности.
        /// </summary>
        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Дата/время последнего обновления сущности.
        /// </summary>
        [BsonElement("updatedDate")]
        public DateTime UpdatedDate { get; set; }
    }
}