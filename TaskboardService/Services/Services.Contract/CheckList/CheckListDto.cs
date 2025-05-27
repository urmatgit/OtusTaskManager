namespace Services.Contract.CheckList
{
    /// <summary>
    /// Чек-лист
    /// </summary>
    public class CheckListDto
    {
        /// <summary>
        /// Идентифифкатор чек-листа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название чек-листа
        /// </summary>
        public required string Name { get; set; }        
    }
}
