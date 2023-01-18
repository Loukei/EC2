using EC2.Models;

namespace EC2.Service
{
    public interface IProductService
    {
        Product Create(ProductViewModel product);
        ProductPagingResponseModel GetAll(string? name, int? supplierID, int? categoryID, int pageIndex = 1, int pageSize = 10);
        Product Get(int productId);
        Product Update(int productId, ProductViewModel product);
        bool Delete(int productId);
    }
}
