namespace Services.Contract.TaskBoard
{
    public class TaskBoardUpdateDto
    {        
        /// <summary>
        /// Название доски
        /// </summary>
        public string? Name { get; set; }        

        /// <summary>
        /// Статус
        /// </summary>
        public int? Status { get; set; }
    }
}
