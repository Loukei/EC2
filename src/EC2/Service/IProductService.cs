using EC2.Models;
using EC2.Models.DTOs.Northwind;

namespace EC2.Service
{
    public interface IProductService
    {
        ProductReplyVM Create(ProductRequestVM product);
        ProductPagingResponseModel GetPaging(ProductPagingViewModel request);
        ProductReplyVM Get(int productId);
        ProductReplyVM Update(int productId, ProductRequestVM product);
        bool Delete(int productId);
    }
}
