
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
        /// <summary>
        /// Дата/время создания сущности.
        /// </summary>
        DateTime CreatedDate { get; set; }
        /// <summary>
        /// Дата/время последнего обновления сущности.
        /// </summary>
        DateTime UpdatedDate { get; set; }
    }
}