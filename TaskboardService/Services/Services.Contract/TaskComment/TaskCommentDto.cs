namespace Services.Contract.TaskComment
{
    /// <summary>
    /// Комментарий задачи
    /// </summary>
    public class TaskCommentDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Имя автора
        /// </summary>
        public required string Author { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public required DateTime CreateDate { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public Guid TaskId { get; set; }
    }
}
