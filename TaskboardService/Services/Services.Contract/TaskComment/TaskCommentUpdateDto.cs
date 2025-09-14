namespace Services.Contract.TaskComment
{
    public class TaskCommentUpdateDto
    {
        /// <summary>
        /// Имя автора
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string? Text { get; set; }
    }
}
