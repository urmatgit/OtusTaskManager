using BoardService.Domain.Abstraction;
using BoardService.Domain.Enums;

namespace BoardService.Domain.Entity
{
    /// <summary>
    /// Задача
    /// </summary>
    public class TaskItem : IEntity<Guid>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование задачи
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// описание задачи
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Дата создание задачи
        /// </summary>
        public required DateTime CreateDate { get; set; }

        /// <summary>
        /// Срок выполнения задачи
        /// </summary>
        public required DateTime PlanDate { get; set; }

        /// <summary>
        /// Фактический срок выполнения задачи
        /// </summary>
        public DateTime? FactDate { get; set; }

        /// <summary>
        /// Приоритет задачи
        /// </summary>
        public TaskPriority Priority { get; set; }

        /// <summary>
        /// Идентификатор колонки доски задач
        /// </summary>
        public Guid BoardColumnId { get; set; }

        /// <summary>
        /// Колонка доски задач
        /// </summary>
        public BoardColumn BoardColumn { get; set; } = null!;

        /// <summary>
        /// Список исполнителей задачи
        /// </summary>
        //public ICollection<Executor> Executors { get; set; } = [];

        /// <summary>
        /// Список комментариев задачи
        /// </summary>
        public ICollection<TaskComment> Comments { get; set; } = [];

        /// <summary>
        /// Список файлов задачи
        /// </summary>
        public ICollection<BoardFile> Files { get; set; } = [];

        /// <summary>
        /// список чек-листов
        /// </summary>
        public ICollection<CheckList> CheckLists { get; set; } = [];
    }
}
