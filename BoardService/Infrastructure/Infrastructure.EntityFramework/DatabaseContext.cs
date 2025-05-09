using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BoardService.Domain.Entity;

namespace Infrastructure.EntityFramework
{
    class DatabaseContext : DbContext
    {
        public DbSet<BoardColumn> BoardColumns { get; init; }
        public DbSet<CheckItem> CheckItems { get; init; }
        public DbSet<CheckList> CheckLists { get; init; }
        public DbSet<TaskBoard> TaskBoards { get; init; }
        public DbSet<TaskComment> TaskComments { get; init; }
        public DbSet<TaskItem> TaskItems { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BoardColumn>().Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();


            modelBuilder.Entity<CheckItem>().Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();


            modelBuilder.Entity<CheckList>().Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();


            modelBuilder.Entity<TaskBoard>().Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();


            modelBuilder.Entity<TaskComment>().Property(p => p.Author)
                .HasMaxLength(150)
                .IsRequired();
            modelBuilder.Entity<TaskComment>().Property(p => p.Text)
                .HasMaxLength(1000);


            modelBuilder.Entity<TaskItem>().Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
