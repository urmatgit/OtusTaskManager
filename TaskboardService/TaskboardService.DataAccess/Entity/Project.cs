using TaskboardService.DataAccess.Abstraction;

namespace TaskboardService.DataAccess.Entity
{
    public class Project : BaseEntity
    {
        /// <summary>
        /// Доски текущего проекта.
        /// </summary>
        public List<Taskboard> Taskboards { get; set; } = [];
    }
}