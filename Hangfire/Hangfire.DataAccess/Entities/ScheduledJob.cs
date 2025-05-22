
namespace Hangfire.DataAccess.Entities
{
    public class ScheduledJob : BaseEntity
    {
        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        public string CustomJobId { get; set; } = default!;
        /// <summary>
        /// Идентификатор создаваемый hangfire'ом.
        /// </summary>
        public string JobId { get; set; } = default!;
        /// <summary>
        /// Дата и время регистрации задачи.
        /// </summary>
        public DateTime CreatedDateTime { get; set; }
    }
}