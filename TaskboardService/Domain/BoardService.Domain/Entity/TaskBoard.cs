using BoardService.Domain.Abstraction;
using BoardService.Domain.Enums;

namespace BoardService.Domain.Entity
{
    /// <summary>
    /// Доска задач
    /// </summary>
    public class TaskBoard : IEntity<Guid>
    {
        /// <summary>
        /// Идентифифкатор доски задач
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название доски
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public required DateTime CreateDate { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public BoardStatus Status { get; set; }

        /// <summary>
        /// Список колонок доски
        /// </summary>
        public ICollection<BoardColumn> Columns { get; set; } = [];
    }
}
