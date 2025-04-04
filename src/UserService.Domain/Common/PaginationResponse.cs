using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Common
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get;  }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public PaginationResponse(List<T> data, int count, int page, int pageSize)
        {
            Data = data;
            CurrentPage = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

    }

    }
