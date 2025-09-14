namespace Services.Contract.CheckList
{
    public class CheckListAddDto
    {
        /// <summary>
        /// Идентификатор чек-листа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название чек-листа
        /// </summary>
        public required string Name { get; set; }
    }
}
