namespace Services.Contract.TaskItem
{
    /// <summary>
    /// Задача
    /// </summary>
    public class TaskItemDto
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
        public int Priority { get; set; }
    }
}
