using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Entities.Abstraction;

namespace UserService.DataAccess.Persistence
{
    public class TaskboardDbContext: DbContext
    {

        private IPublisher _publisher;

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        
        public TaskboardDbContext(DbContextOptions<TaskboardDbContext> dbContextOptions,IPublisher publisher): base(dbContextOptions)
        {
            _publisher = publisher;    
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
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result=await base.SaveChangesAsync(cancellationToken);
            await  PublishDomainEventsAsync().ConfigureAwait(false);
            return result;
        }

        private async Task PublishDomainEventsAsync()
        {
            var DomainEvents = ChangeTracker.Entries<IEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Count > 0)
                .SelectMany(e =>
                {
                    var domainEvents = e.DomainEvents.ToList();
                    e.DomainEvents.Clear();
                    return domainEvents;
                })
                .ToList();
            
                foreach (var domainEvent in DomainEvents)
                {
                    await _publisher.Publish(domainEvent).ConfigureAwait(false);
                }
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
