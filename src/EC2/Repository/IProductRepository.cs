using NorthWindLibrary.DTOs;
using EC2.Models;
using X.PagedList;

namespace EC2.Repository
{
    public interface IProductRepository
    {
        Product Create(ProductUpdateVM product);
        Product GetByID(int productId);
        IPagedList<Product> GetPaging(ProductPageQueryVM parameters);
        int CountByQuery(ProductPageQueryVM parameters);
        Product Update(int productId, ProductUpdateVM product);
        bool Delete(int productId);
    }
}
