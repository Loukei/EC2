using EC2.Models;
using EC2.Models.DTOs.Northwind;

namespace EC2.Service
{
    public interface IProductService
    {
        Product Create(ProductRequestVM product);
        ProductPagingResponseModel GetPaging(ProductPagingViewModel request);
        Product Get(int productId);
        Product Update(int productId, ProductRequestVM product);
        bool Delete(int productId);
    }
}
