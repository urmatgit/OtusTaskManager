using BoardService.Domain.Abstraction;

namespace BoardService.Domain.Entity
{
    /// <summary>
    /// Чек-лист
    /// </summary>
    public class CheckList : IEntity<Guid>
    {
        /// <summary>
        /// Идентифифкатор чек-листа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название чек-листа
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Элементы чек-листа
        /// </summary>
        public ICollection<CheckItem> CheckItems { get; set; } = [];

        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Задача
        /// </summary>
        public TaskItem TaskItem { get; set; } = null!;
    }
}
