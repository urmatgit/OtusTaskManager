using MongoDB.Bson.Serialization.Attributes;

using TaskboardService.DataAccess.Abstraction;

namespace TaskboardService.DataAccess.Entity
{
    /// <summary>
    /// Элемент чек-листа
    /// </summary>
    public class CheckItem : BaseEntity
    {
        /// <summary>
        /// Название элемента
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Флаг выбранного элемента в чек-листе.
        /// </summary>
        [BsonElement("flag")]
        public bool Flag { get; set; }
    }
}