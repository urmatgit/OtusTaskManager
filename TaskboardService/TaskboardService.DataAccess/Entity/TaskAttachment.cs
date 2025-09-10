using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using TaskboardService.DataAccess.Abstraction;

namespace TaskboardService.DataAccess.Entity
{
    /// <summary>
    /// Файл, прикрепляемый к задаче/комментарию.
    /// </summary>
    public class TaskAttachment : BaseEntity
    {
        /// <summary>
        /// Название файла.        
        /// </summary>
        [BsonElement("fileName")]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Расширение сохранённого файла.
        /// </summary>
        [BsonElement("extension")]
        public string Extension { get; set; } = string.Empty;

        /// <summary>
        /// Формат файла для отображения на клиенте.
        /// </summary>
        [BsonElement("format")]
        public string Format { get; set; } = string.Empty;

        /// <summary>
        /// Размер файла в килобайтах.
        /// </summary>
        [BsonElement("fileSize")]
        public long FileSize { get; set; } = 0;

        /// <summary>
        /// Идентификатор файла в хранилище
        /// </summary>
        [BsonElement("fileStorageId")]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid FileStorageId { get; set; }
    }
}