namespace EC2.Models
{
    public class PagedResults<T>
    {
        public int TotalRecords { get; set; } = 0;
        public int CurrentPageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public bool HasNextPage { get; set; } = false;
        public bool HasPreviousPage { get; set; } = false;
        public List<T> Records { get; set; }

        public PagedResults(List<T> data, int totalRecords, int currentPageNumber, int pageSize, int totalPages)
        {
            Records = data;
            TotalRecords = totalRecords;
            CurrentPageNumber = currentPageNumber;
            PageSize = pageSize;

            TotalPages = Convert.ToInt32(Math.Ceiling(((double)TotalRecords / (double)pageSize)));
            HasNextPage = currentPageNumber < totalPages;
            HasPreviousPage = CurrentPageNumber > 1;
        }
    }
}
