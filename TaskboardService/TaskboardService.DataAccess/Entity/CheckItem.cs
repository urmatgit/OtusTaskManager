using TaskboardService.DataAccess.Abstraction;

namespace TaskboardService.DataAccess.Entity
{
    /// <summary>
    /// Элемент чек-листа
    /// </summary>
    public class CheckItem : BaseEntity
    {
        /// <summary>
        /// Название элемента
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Флаг выбранного элемента в чек-листе.
        /// </summary>
        public bool Flag { get; set; }
    }
}