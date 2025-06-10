
namespace TaskboardService.DataAccess.Abstraction
{
    /// <summary>
    /// Базовый класс сущностей.
    /// </summary>
    public abstract class BaseEntity : IBaseEntity<Guid>
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public Guid Id { get; set; }
    }
}