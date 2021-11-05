using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Shared.Models
{
    /// <summary>
    /// Represents a data collection as a pages, it holds the data of the page in addition to the count of the total items, count of total pages and the current page index
    /// </summary>
    /// <typeparam name="T">Type of the data that will be hold in the PageList</typeparam>
    public class PagedList<T>
    {
        public PagedList(IEnumerable<T> items, int pageIndex, int pagesCount, int pageSize, int totalItemsCount)
        {
            Items = items;
            PageIndex = pageIndex;
            PagesCount = pagesCount;
            PageSize = pageSize;
            TotalItemsCount = totalItemsCount;
        }

        public IEnumerable<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int PagesCount { get; set; }

        public int PageSize { get; set; }

        public int TotalItemsCount { get; set; }

    }
}
