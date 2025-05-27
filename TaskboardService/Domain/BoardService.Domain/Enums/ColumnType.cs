using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Domain.Enums
{
    /// <summary>
    /// Тип колонки
    /// </summary>
    public enum ColumnType
    {
        /// <summary>
        /// Бэклог
        /// </summary>
        Backlog = 0,

        /// <summary>
        /// В работе
        /// </summary>
        AtWork = 1,

        /// <summary>
        /// Выполнено
        /// </summary>
        Completed = 2
    }
}
