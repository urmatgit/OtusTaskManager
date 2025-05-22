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
    /// Колонка доски задач
    /// </summary>
    public class BoardColumn : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Название колонки
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Дата создание колонки
        /// </summary>
        public required DateTime CreateDate { get; set; }

        /// <summary>
        /// Тип колонки
        /// </summary>
        public ColumnType ColumnType { get; set; }

        /// <summary>
        /// Вип-лимит
        /// </summary>
        public int VipLimit { get; set; }
        // TODO Что такое вип-лимит?

        /// <summary>
        /// Список задач
        /// </summary>
        public IEnumerable<TaskItem>? Tasks { get; set; }
    }
}
