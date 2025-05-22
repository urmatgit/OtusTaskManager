using Hangfire.DataAccess.Config;
using Hangfire.DataAccess.Entities;

using Microsoft.EntityFrameworkCore;

namespace Hangfire.DataAccess
{
    public class HangfireDataContext(DbContextOptions options) : DbContext(options)
    {
        private const string schemaName = "dbo";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schemaName);

            modelBuilder.ApplyConfiguration(new ProjectConfig());
            modelBuilder.ApplyConfiguration(new ScheduledJobConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserProjectConfig());
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ScheduledJob> ScheduledJobs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
    }
}