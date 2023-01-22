using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EC2.Models.EFcore;

namespace EC2.Models
{
    /// <summary>
    /// 用來回傳Batch處理的結果
    /// </summary>
    public class ProductPagingResponseModel
    {
        public int TotalRecords { get; set; } = 0;
        public int CurrentPageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public bool HasNextPage { get; set; } = false;
        public bool HasPreviousPage { get; set; } = false;
        public IEnumerable<Product> Data { get; set; }

        public ProductPagingResponseModel(IEnumerable<Product> data, int totalRecords, int currentPageNumber, int pageSize, int totalPages)
        {
            Data = data;
            TotalRecords = totalRecords;
            CurrentPageNumber = currentPageNumber;
            PageSize = pageSize;

            TotalPages = Convert.ToInt32(Math.Ceiling(((double)TotalRecords / (double)pageSize)));
            HasNextPage = currentPageNumber < totalPages;
            HasPreviousPage = CurrentPageNumber > 1;
        }
    }
}
