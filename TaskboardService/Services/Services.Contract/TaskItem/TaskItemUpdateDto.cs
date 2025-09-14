namespace Services.Contract.TaskItem
{
    public class TaskItemUpdateDto
    {        
        /// <summary>
        /// Наименование задачи
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// описание задачи
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Срок выполнения задачи
        /// </summary>
        public DateTime? PlanDate { get; set; }

        /// <summary>
        /// Фактический срок выполнения задачи
        /// </summary>
        public DateTime? FactDate { get; set; }

        /// <summary>
        /// Приоритет задачи
        /// </summary>
        public int? Priority { get; set; }        
    }
}
