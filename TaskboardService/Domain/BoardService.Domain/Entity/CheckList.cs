using BoardService.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IEnumerable<CheckItem>? CheckItems { get; set; }

        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Задача
        /// </summary>
        public required TaskItem Task { get; set; }
    }
}
