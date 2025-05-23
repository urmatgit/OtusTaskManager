namespace Hangfire.DataAccess.Entities
{
    public class UserProject : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }
}