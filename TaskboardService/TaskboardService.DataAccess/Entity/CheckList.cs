using MongoDB.Bson.Serialization.Attributes;

using TaskboardService.DataAccess.Abstraction;

namespace TaskboardService.DataAccess.Entity
{
    /// <summary>
    /// Чек-лист
    /// </summary>
    public class CheckList : BaseEntity
    {
        /// <summary>
        /// Название чек-листа
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Элементы чек-листа
        /// </summary>
        [BsonElement("checkItems")]
        public List<CheckItem> CheckItems { get; set; } = [];
    }
}