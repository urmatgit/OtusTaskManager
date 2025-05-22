using BoardService.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Domain.Entity
{
    /// <summary>
    /// Комментарий задачи
    /// </summary>
    public class TaskComment : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Имя автора
        /// </summary>
        public required string Author { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public required DateTime CreateDate { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string? Text { get; set; }

    }
}
