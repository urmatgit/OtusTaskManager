using TaskboardService.DataAccess.Abstraction;

namespace TaskboardService.DataAccess.Entity
{
    public class TaskboardUser : BaseEntity
    {
        /// <summary>
        /// Полное имя пользователя
        /// </summary>
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}