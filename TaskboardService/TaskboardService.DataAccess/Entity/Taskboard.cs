using TaskboardService.DataAccess.Abstraction;
using TaskboardService.DataAccess.Enums;

namespace TaskboardService.DataAccess.Entity
{
    /// <summary>
    /// Доска задач
    /// </summary>
    public class Taskboard : BaseEntity
    {
        /// <summary>
        /// Порядок сортировки досок в списке.
        /// </summary>
        public float SortOrder { get; set; }

        /// <summary>
        /// Название доски
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public TaskboardStatus Status { get; set; }

        /// <summary>
        /// Список колонок доски
        /// </summary>
        public List<TaskboardColumn> Columns { get; set; } = [];
    }
}