using EC2.Models;
using EC2.Models.DTOs.Northwind;

namespace EC2.Service
{
    public interface IProductService
    {
        ProductResultVM Create(ProductRequestVM product);
        PagedResultsVM<ProductResultVM> GetPaging(ProductPagingVM request);
        ProductResultVM Get(int productId);
        ProductResultVM Update(int productId, ProductRequestVM product);
        bool Delete(int productId);
    }
}
