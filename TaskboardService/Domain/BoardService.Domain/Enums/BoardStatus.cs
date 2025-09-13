using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Domain.Enums
{
    /// <summary>
    /// Статус доски задач
    /// </summary>
    public enum BoardStatus
    {
        /// <summary>
        /// В работе
        /// </summary>
        AtWork = 0,

        /// <summary>
        /// Архив
        /// </summary>
        Archive
    }
}
