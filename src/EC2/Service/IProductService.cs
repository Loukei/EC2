using EC2.Models;
using EC2.Models.DTOs.Northwind;

namespace EC2.Service
{
    public interface IProductService
    {
        ProductVM Create(ProductUpdateVM product);
        PPagedList<ProductVM> GetPaging(ProductPageQueryVM request);
        ProductVM Get(int productId);
        ProductVM Update(int productId, ProductUpdateVM product);
        bool Delete(int productId);
    }
}
