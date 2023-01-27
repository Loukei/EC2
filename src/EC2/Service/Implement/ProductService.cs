using Microsoft.AspNetCore.Mvc;
using EC2.Models;
using EC2.Models.EFCore;
using EC2.Repository;

namespace EC2.Service.Implement
{

    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ISuppilierRepository _suppilierRepo;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepo, 
            ICategoryRepository categoryRepo, 
            ISuppilierRepository suppilierRepo,
            ILogger<ProductService> logger)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _suppilierRepo = suppilierRepo;
            _logger = logger;   
        }

        public Product Create(ProductViewModel product)
        {
            try
            {
                if (_suppilierRepo.GetSuppilierByID(product.SupplierID) == null ||
                _categoryRepo.GetCategoryByID(product.CategoryID) == null)
                {
                    throw new Exception($"SupplierID {product.SupplierID} or CategoryID {product.CategoryID} not exist!");
                }
                var result = _productRepo.Create(product);
                if (result == null)
                    throw new Exception("Create product has failed");
                return result;
            } 
            catch(Exception tex)
            {
                _logger.LogError($"unexpected error: {tex.Message}");
                return null;
            }
        }

        public ProductPagingResponseModel GetPaging(string? name, int? supplierID, int? categoryID, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                if (
                    (
                        supplierID.HasValue
                        &&
                        _suppilierRepo.GetSuppilierByID(supplierID.Value) == null
                    )
                    ||
                    (
                        categoryID.HasValue
                        &&
                        _categoryRepo.GetCategoryByID(categoryID.Value) == null
                     )
                    )
                {
                    throw new Exception($"SupplierID {supplierID} or CategoryID {categoryID} not exist!");
                }
                var pagingresults = _productRepo.GetPaging(name, supplierID, categoryID, pageIndex, pageSize);
                if (pagingresults == null)
                    throw new Exception("No Products found.");
                return pagingresults;
            }
            catch (Exception tex)
            {
                _logger.LogError($"unexpected error: {tex.Message}");
                return null;
            }
        }

        public Product Get(int productId)
        {
            try
            {
                var product = _productRepo.GetByID(productId);
                if (product == null)
                    throw new Exception($"Product {productId} does not exist.");
                return product;
            }
            catch (Exception tex)
            {
                /// 處裡連線失敗or其他
                _logger.LogError($"unexpected error: {tex.Message}");
                return null;
            }
        }

        public Product Update(int productId, ProductViewModel product)
        {
            try
            {
                if (_suppilierRepo.GetSuppilierByID(product.SupplierID) == null
                || _categoryRepo.GetCategoryByID(product.CategoryID) == null)
                {
                    throw new Exception("SupplierID or CategoryID not exist!");
                }
                var newProduct = _productRepo.Update(productId, product);
                if (newProduct == null)
                    throw new Exception($"Can't update Product {productId}");
                return newProduct;
            }
            catch (Exception tex)
            {
                _logger.LogError($"unexpected error: {tex.Message}");
                return null;
            }
        }

        public bool Delete(int productId)
        {
            try
            {
                bool hasDelete  = _productRepo.Delete(productId);
                if (!hasDelete)
                    throw new Exception($"Failed to delete product {productId}");
                return hasDelete;
            }
            catch (Exception tex)
            {
                /// 處裡連線失敗or其他
                _logger.LogError($"unexpected error: {tex.Message}");
                return false;
            }
        }
    }
}
