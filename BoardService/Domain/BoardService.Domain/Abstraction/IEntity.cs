using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Domain.Abstraction
{
    /// <summary>
    /// Интерфейс сущности с идентификатором
    /// </summary>
    /// <typeparam name="T">Тип идентификатора</typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        T Id { get; set; }
    }
}
