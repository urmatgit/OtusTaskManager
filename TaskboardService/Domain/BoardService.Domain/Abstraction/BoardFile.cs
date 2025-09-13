namespace BoardService.Domain.Abstraction
{
    /// <summary>
    /// Файл
    /// </summary>
    public class BoardFile : IEntity<Guid>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public required string FileName { get; set; }
    }
}
