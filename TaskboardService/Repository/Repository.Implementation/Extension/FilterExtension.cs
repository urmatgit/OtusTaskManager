using BoardService.Domain.Abstraction;

namespace Repository.Implementation.Extension
{
    public static class FilterExtension
    {
        public static IQueryable<T> AddFilter<T>(this IQueryable<T> query, IPageFilter filter)
        {
            return query
                .Skip((filter.Page - 1) * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage);
        }
    }
}
