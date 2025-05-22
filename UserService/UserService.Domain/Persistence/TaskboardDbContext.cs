using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence
{
    public class TaskboardDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public TaskboardDbContext(DbContextOptions<TaskboardDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }
        static TaskboardDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //добавляем конфиграции 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskboardDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
