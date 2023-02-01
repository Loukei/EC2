using X.PagedList;

namespace EC2.Models
{
    /// <summary>
    /// A DTO Container for Service/Controller layer return pagedResults query.
    /// <seealso cref="X.PagedList.IPagedList"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PPagedList<T>: IPagedList
    {
        public PPagedList(PagedListMetaData matadata, List<T> items)
        {
            /// Set matadata
            TotalItemCount = matadata.TotalItemCount;
            PageCount = matadata.PageCount;
            /// Set Items
            this.Items = items;
        }

        public int PageCount { get; set; }

        public int TotalItemCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

        public bool IsFirstPage { get; set; }

        public bool IsLastPage { get; set; }

        public int FirstItemOnPage { get; set; }

        public int LastItemOnPage { get; set; }
        public IList<T> Items { get; set; }
    }
}
