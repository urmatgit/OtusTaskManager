using BoardService.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        //public DbSet<BoardColumn> BoardColumns { get; init; }
        //public DbSet<CheckItem> CheckItems { get; init; }
        //public DbSet<CheckList> CheckLists { get; init; }
        public DbSet<TaskBoard> TaskBoards { get; init; }
        //public DbSet<TaskComment> TaskComments { get; init; }
        //public DbSet<TaskItem> TaskItems { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // колонка доски задач
            modelBuilder.Entity<BoardColumn>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<BoardColumn>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<BoardColumn>()
                .HasMany(x => x.Tasks)
                .WithOne(x => x.BoardColumn)
                .HasForeignKey(x => x.BoardColumnId)
                .OnDelete(DeleteBehavior.Restrict);


            // Элемент чек-листа
            modelBuilder.Entity<CheckItem>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<CheckItem>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();            

            // чек-лист
            modelBuilder.Entity<CheckList>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<CheckList>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<CheckList>()
                .HasMany(x => x.CheckItems)
                .WithOne(x => x.CheckList)
                .HasForeignKey(x => x.CheckListId)
                .OnDelete(DeleteBehavior.Restrict);


            // доска задач            
            modelBuilder.Entity<TaskBoard>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<TaskBoard>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<TaskBoard>()
                .HasMany(x => x.Columns)
                .WithOne(x => x.TaskBoard)
                .HasForeignKey(x => x.TaskBoardId)
                .OnDelete(DeleteBehavior.Restrict);


            // комментарий задачи
            modelBuilder.Entity<TaskComment>()
                .HasKey (x => x.Id);
            modelBuilder.Entity<TaskComment>()
                .Property(p => p.Author)
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
            modelBuilder.Entity<TaskItem>()
                .HasKey (x => x.Id);
            modelBuilder.Entity<TaskItem>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<TaskItem>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Tasktem)
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TaskItem>()
                .HasMany(x => x.CheckLists)
                .WithOne(x => x.TaskItem)
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
