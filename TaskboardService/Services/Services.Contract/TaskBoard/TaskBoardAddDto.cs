namespace Services.Contract.TaskBoard
{
    public class TaskBoardAddDto
    {
        /// <summary>
        /// Идентифифкатор доски задач
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название доски
        /// </summary>
        public required string Name { get; set; }                
    }
}
