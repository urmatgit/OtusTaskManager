namespace Services.Contract.TaskBoard
{
    /// <summary>
    /// Доска задач
    /// </summary>
    public class TaskBoardDto
    {
        /// <summary>
        /// Идентифифкатор доски задач
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название доски
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public required DateTime CreateDate { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public int Status { get; set; }        
    }
}
