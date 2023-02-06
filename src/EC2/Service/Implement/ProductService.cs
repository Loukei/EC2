using EC2.Models;
using EC2.Models.DTOs.Northwind;
using EC2.Repository.Implement;
using EC2.Repository;
using AutoMapper;
using X.PagedList;

namespace EC2.Service.Implement
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ISuppilierRepository _suppilierRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepo, 
            ICategoryRepository categoryRepo, 
            ISuppilierRepository suppilierRepo,
            IMapper mapper,
            ILogger<ProductService> logger)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _suppilierRepo = suppilierRepo;
            _logger = logger;
            _mapper = mapper;
        }

        public ProductVM Create(ProductUpdateVM product)
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
                return _mapper.Map<ProductVM>(result);
            } 
            catch(Exception tex)
            {
                _logger.LogError($"unexpected error: {tex.Message}");
                return null;
            }
        }

        public PPagedList<ProductVM> GetPaging(ProductPageQueryVM request)
        {
            try
            {
                if (
                    (
                        request.supplierID.HasValue
                        &&
                        _suppilierRepo.GetSuppilierByID(request.supplierID.Value) == null
                    )
                    ||
                    (
                        request.categoryID.HasValue
                        &&
                        _categoryRepo.GetCategoryByID(request.categoryID.Value) == null
                     )
                    )
                {
                    throw new Exception($"SupplierID {request.supplierID} or CategoryID {request.categoryID} not exist!");
                }

                IPagedList<Product> productRepoPagedResults = _productRepo.GetPaging(request);
                if (productRepoPagedResults == null)
                {
                    throw new Exception("No Products found.");
                }

                /// IPagedList<Product> to PPagedList<ProductVM>
                var metadata = productRepoPagedResults.GetMetaData();
                var items = _mapper.Map<List<ProductVM>>(productRepoPagedResults.ToList());
                return new PPagedList<ProductVM>(metadata, items);
            }
            catch (Exception tex)
            {
                _logger.LogError($"unexpected error: {tex.Message}");
                return null;
            }
        }

        public ProductVM Get(int productId)
        {
            try
            {
                var product = _productRepo.GetByID(productId);
                if (product == null)
                    throw new Exception($"Product {productId} does not exist.");
                return _mapper.Map<Product, ProductVM>(product);
            }
            catch (Exception tex)
            {
                /// 處裡連線失敗or其他
                _logger.LogError($"unexpected error: {tex.Message}");
                return null;
            }
        }

        public ProductVM Update(int productId, ProductUpdateVM product)
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
                return _mapper.Map<ProductVM>(newProduct);
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
