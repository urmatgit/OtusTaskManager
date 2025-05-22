using BoardService.Domain.Abstraction;
using BoardService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public BoartStatus Status { get; set; }

        /// <summary>
        /// Список колонок доски
        /// </summary>
        public IEnumerable<BoardColumn>? Columns { get; set; }
    }
}
