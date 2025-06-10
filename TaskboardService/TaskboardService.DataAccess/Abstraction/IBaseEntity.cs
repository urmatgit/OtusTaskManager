
namespace TaskboardService.DataAccess.Abstraction
{
    /// <summary>
    /// Интерфейс сущности с идентификатором
    /// </summary>
    /// <typeparam name="T">Тип идентификатора</typeparam>
    public interface IBaseEntity<T>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        T Id { get; set; }
    }
}