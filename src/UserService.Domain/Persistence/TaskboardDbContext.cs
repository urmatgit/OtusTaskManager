using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Entities.Identity;

namespace UserService.DataAccess.Persistence
{
    public class TaskboardDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public TaskboardDbContext(DbContextOptions<TaskboardDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //добавляем конфиграции 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskboardDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
