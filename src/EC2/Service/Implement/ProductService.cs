using Microsoft.AspNetCore.Mvc;
using EC2.Models;
using EC2.Repository;
using EC2.Models.DTOs.Northwind;
using AutoMapper;

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

        public ProductResultVM Create(ProductRequestVM product)
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
                return _mapper.Map<ProductResultVM>(result);
            } 
            catch(Exception tex)
            {
                _logger.LogError($"unexpected error: {tex.Message}");
                return null;
            }
        }

        public PagedResultsVM<ProductResultVM> GetPaging(ProductPagingVM request)
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

                var pagingresults = _productRepo.GetPaging(
                    request.name, 
                    request.supplierID, 
                    request.categoryID,
                    request.pageIndex,
                    request.pageSize);
                if (pagingresults == null)
                {
                    throw new Exception("No Products found.");
                }

                /// TODO: use automapper turn 
                /// PagedResultsVM<Product> to PagedResultsVM<ProductResultVM>
                return _mapper.Map<PagedResultsVM<Product>, PagedResultsVM<ProductResultVM>>(pagingresults);
                //List<ProductRequestVM> products = _mapper.Map<List<ProductRequestVM>>(pagingresults.Records);
            }
            catch (Exception tex)
            {
                _logger.LogError($"unexpected error: {tex.Message}");
                return null;
            }
        }

        public ProductResultVM Get(int productId)
        {
            try
            {
                var product = _productRepo.GetByID(productId);
                if (product == null)
                    throw new Exception($"Product {productId} does not exist.");
                return _mapper.Map<Product, ProductResultVM>(product);
            }
            catch (Exception tex)
            {
                /// 處裡連線失敗or其他
                _logger.LogError($"unexpected error: {tex.Message}");
                return null;
            }
        }

        public ProductResultVM Update(int productId, ProductRequestVM product)
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
                return _mapper.Map<ProductResultVM>(newProduct);
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
