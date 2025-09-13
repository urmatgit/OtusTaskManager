namespace BoardService.Domain.Abstraction
{
    /// <summary>
    /// Исполнитель
    /// </summary>
    public class Executor : IEntity<Guid>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public required string LastName { get; set; }
    }
}
