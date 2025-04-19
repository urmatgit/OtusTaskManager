namespace Hangfire.DataAccess.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }

        public Guid UserId { get; set; }
        //Owner or creator
        public Project User { get; set; }
        public IList<User> Users { get; set; } = new List<User>();

    }
}