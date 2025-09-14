namespace Services.Contract.TaskComment
{
    public class TaskCommentAddDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Имя автора
        /// </summary>
        public required string Author { get; set; }        

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string? Text { get; set; }        
    }
}
