using BoardService.Domain.Abstraction;
using BoardService.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DateTime FactDate { get; set; }

        /// <summary>
        /// Приоритет задачи
        /// </summary>
        public TaskPriority Priority { get; set; }

        /// <summary>
        /// Список исполнителей задачи
        /// </summary>
        public IEnumerable<IExecutor>? Executors { get; set; }

        /// <summary>
        /// Список комментариев задачи
        /// </summary>
        public IEnumerable<TaskComment>? Comments { get; set; }

        /// <summary>
        /// Список файлов задачи
        /// </summary>
        public IEnumerable<IFile>? Files { get; set; }

        /// <summary>
        /// список чек-листов
        /// </summary>
        public IEnumerable<CheckList>? CheckLists { get; set; }
    }
}
