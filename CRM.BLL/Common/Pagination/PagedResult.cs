using System.Collections.Generic;

namespace CRM.BLL.Common.Pagination
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; set; } = new List<T>();

        public int TotalCount { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages
        {
            get
            {
                if (PageSize <= 0) return 0;
                return (int)System.Math.Ceiling((double)TotalCount / PageSize);
            }
        }
    }
}
