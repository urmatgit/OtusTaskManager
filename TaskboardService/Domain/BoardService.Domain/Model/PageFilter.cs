using BoardService.Domain.Abstraction;

namespace BoardService.Domain.Model
{
    /// <summary>
    /// Страничный фильтр 
    /// </summary>
    /// <param name="Page">Номер страницы</param>
    /// <param name="ItemsPerPage">Размер страницы</param>
    public record class PageFilter(int Page, int ItemsPerPage) : IPageFilter
    {
        private static readonly int DEFAULT_PAGE = 1;
        private static readonly int DEFAULT_ITEMS_PER_PAGE = 5;

        /// <summary>
        /// Фильтр по умолчанию
        /// </summary>
        /// <param name="Page">Номер страницы</param>
        /// <param name="ItemsPerPage">Размер страницы</param>
        /// <returns></returns>
        public static PageFilter Default(int? Page, int? ItemsPerPage)
        {
            var page = int.Max(Page ?? DEFAULT_PAGE, DEFAULT_PAGE);
            var itemsPerPage = int.Max(ItemsPerPage ?? DEFAULT_ITEMS_PER_PAGE, 1);

            return new PageFilter(page, itemsPerPage);
        }
    }
}
