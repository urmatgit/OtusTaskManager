namespace Services.Contract.CheckItem
{
    /// <summary>
    /// Элемент чек-листа
    /// </summary>
    public class CheckItemDto
    {
        /// <summary>
        /// Идентифифкатор элемента чек-листа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название элемента
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Флаг
        /// </summary>        
        // TODO: Что такое флаг, для чего нужен?
        public bool Flag { get; set; }
    }
}
