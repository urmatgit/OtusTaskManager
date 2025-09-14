namespace Services.Contract.BorderColumn
{
    public class BoardColumnUpdateDto
    {
        /// <summary>
        /// Название колонки
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Тип колонки
        /// </summary>
        public int? ColumnType { get; set; }

        /// <summary>
        /// Вип-лимит
        /// </summary>
        public int? VipLimit { get; set; }        
    }
}
