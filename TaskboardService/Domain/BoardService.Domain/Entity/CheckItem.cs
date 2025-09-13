using BoardService.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Domain.Entity
{
    /// <summary>
    /// Элемент чек-листа
    /// </summary>
    public class CheckItem : IEntity<Guid>
    {
        /// <summary>
        /// Идентифифкатор элемента чек-листа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название элемента
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Флаг
        /// </summary>        
        // TODO: Что такое флаг, для чего нужен?
        public bool Flag { get; set; }

        /// <summary>
        /// Идентификатор чек-листа
        /// </summary>
        public Guid CheckListId { get; set; }

        /// <summary>
        /// Чек-лист
        /// </summary>
        public CheckList CheckList { get; set; } = null!;
    }
}
