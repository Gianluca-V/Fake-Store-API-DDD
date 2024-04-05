using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Models
{
    public class PagedList<T>
    {
        private PagedList(List<T> items, int page, int pageSize, int totalItemCount, int totalPageCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            TotalPageCount = totalPageCount;
        }

        public static PagedList<T> Create(List<T> items, int page, int pageSize)
        {
            int totalItemCount = items.Count;
            int totalPageCount = (int)Math.Ceiling((decimal)totalItemCount / pageSize);

            return new(items,page,pageSize,totalItemCount,totalPageCount); 
        }

        public List<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalItemCount { get; }
        public int TotalPageCount {  get; }
        public bool HasNextPage => Page * PageSize < TotalItemCount;
        public bool HasPrevPage => Page > 1;
    }
}
