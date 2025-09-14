namespace BoardService.Domain.Abstraction
{
    public interface IPageFilter
    {
        /// <summary>
        /// Номер страницы, начиная с 1
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Кол-во элементов на странице, должно быть больше 0
        /// </summary>
        public int ItemsPerPage { get; }
    }
}
