namespace Services.Contract.BorderColumn
{
    public class BoardColumnAddDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Название колонки
        /// </summary>
        public required string Name { get; set; }        

        /// <summary>
        /// Тип колонки
        /// </summary>
        public int ColumnType { get; set; }

        /// <summary>
        /// Вип-лимит
        /// </summary>
        public int VipLimit { get; set; }        
    }
}
