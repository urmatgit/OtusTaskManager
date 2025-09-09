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
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Расширение сохранённого файла.
        /// </summary>
        public string Extension { get; set; } = string.Empty;

        /// <summary>
        /// Формат файла для отображения на клиенте.
        /// </summary>
        public string Format { get; set; } = string.Empty;

        /// <summary>
        /// Размер файла в килобайтах.
        /// </summary>
        public required long FileSize { get; set; } = 0;
    }
}