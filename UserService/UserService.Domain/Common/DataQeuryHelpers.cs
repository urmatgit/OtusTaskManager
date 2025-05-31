using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Common
{
    public static class DataQeuryHelpers
    {
        public static IQueryable<T> PaginateBy<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            if (pageIndex > 1)
            {
                query = query.Skip((pageIndex - 1) * pageSize);
            }

            return query
                .Take(pageSize);

        }
    }
}
