using BoardService.Domain.Entity;
using Microsoft.EntityFrameworkCore;

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

            // колонка доски задач
            modelBuilder.Entity<BoardColumn>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<BoardColumn>()
                .HasOne(x => x.TaskBoard)
                .WithMany()
                .HasForeignKey(x => x.TaskBoardId)
                .OnDelete(DeleteBehavior.Restrict);


            // Элемент чек-листа
            modelBuilder.Entity<CheckItem>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<CheckItem>()
                .HasOne<CheckList>()
                .WithMany()
                .HasForeignKey(x => x.CheckListId)
                .OnDelete(DeleteBehavior.Restrict);


            // чек-лист
            modelBuilder.Entity<CheckList>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<CheckList>()
                .HasOne<TaskItem>()
                .WithMany()
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Restrict);


            // доска задач            
            modelBuilder.Entity<TaskBoard>().Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();


            // комментарий задачи
            modelBuilder.Entity<TaskComment>().Property(p => p.Author)
                .HasMaxLength(150)
                .IsRequired();
            modelBuilder.Entity<TaskComment>().Property(p => p.Text)
                .HasMaxLength(1000);
            modelBuilder.Entity<TaskComment>()
                .HasOne<TaskItem>()
                .WithMany()
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Restrict);


            // задача
            modelBuilder.Entity<TaskItem>().Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<TaskItem>()
                .HasOne<TaskItem>()
                .WithMany()
                .HasForeignKey(x => x.BoardColumnId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
