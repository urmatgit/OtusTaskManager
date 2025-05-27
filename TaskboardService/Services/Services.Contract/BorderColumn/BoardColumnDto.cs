namespace Services.Contract.BorderColumn
{
    /// <summary>
    /// Dto колонки доски задач
    /// </summary>
    public class BoardColumnDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Название колонки
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Дата создание колонки
        /// </summary>
        public required DateTime CreateDate { get; set; }

        /// <summary>
        /// Тип колонки
        /// </summary>
        public int ColumnType { get; set; }

        /// <summary>
        /// Вип-лимит
        /// </summary>
        public int VipLimit { get; set; }
        // TODO Что такое вип-лимит?        
    }
}
