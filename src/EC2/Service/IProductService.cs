using EC2.Models;
using EC2.Models.EFCore;

namespace EC2.Service
{
    public interface IProductService
    {
        Product Create(ProductViewModel product);
        ProductPagingResponseModel GetPaging(ProductPagingViewModel request);
        Product Get(int productId);
        Product Update(int productId, ProductViewModel product);
        bool Delete(int productId);
    }
}
