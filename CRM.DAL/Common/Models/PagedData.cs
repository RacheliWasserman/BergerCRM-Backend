using System.Collections.Generic;

namespace CRM.DAL.Common.Models
{
    public class PagedData<T>
    {
        public IReadOnlyList<T> Items { get; set; } = new List<T>();

        public int TotalCount { get; set; }
    }
}
