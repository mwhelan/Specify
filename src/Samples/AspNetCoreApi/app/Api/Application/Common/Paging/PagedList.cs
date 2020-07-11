using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTemplate.Api.Application.Common.Paging
{
    public class PagedList<TDto>
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public List<TDto> Results { get; set; }

        public PagedList() { }

        public PagedList(int page, int? pageSize, List<TDto> data)
        {
            CurrentPage = page;
            RowCount = data.Count();
            PageSize = pageSize == null ? RowCount : (int)pageSize; // If we don't pass in a page size then we want it all

            var pageCount = (double)RowCount / (PageSize == 0 ? 25 : PageSize); // Default our page size to 25 if we end up with zero
            PageCount = (int)Math.Ceiling(pageCount);
            if (PageCount == 0)
                PageCount = 1;

            if (PageCount < CurrentPage)
                CurrentPage = PageCount;

            var skip = (CurrentPage - 1) * PageSize;

            Results = data.Skip(skip).Take(PageSize).ToList();
        }
    }
}
