using EC2.Models;
using EC2.Context;
using Microsoft.EntityFrameworkCore;
using EC2.Models.DTOs.Northwind;
using AutoMapper;

namespace EC2.Repository
{
    public interface IProductRepository {
        Product Create(ProductRequestVM product);
        Product GetByID(int productId);
        PagedResultsVM<Product> GetPaging(ProductPagingVM parameters);
        Product Update(int productId, ProductRequestVM product);        
        bool Delete(int productId);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext _northwindContext;
        private readonly IMapper _mapper;
        public ProductRepository(NorthwindContext northwindContext, IMapper mapper)
        {
            _northwindContext = northwindContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Turn ProductViewModel to Product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Product ModelToProduct(ProductRequestVM model)
        {
            var product = new Product
            {
                ProductName = model.ProductName,
                SupplierId = model.SupplierID,
                CategoryId = model.CategoryID,
                QuantityPerUnit = model.QuantityPerUnit,
                UnitPrice = model.UnitPrice,
                UnitsInStock = (short?)model.UnitsInStock,
                UnitsOnOrder = (short?)model.UnitsOnOrder,
                ReorderLevel = (short?)model.ReorderLevel,
                Discontinued = model.Discontinued != 0,
                /// Set UpdateBy admin user
                UpdatedBy = 1,
                Status = true,
            };
            return product;
        }

        /// <summary>
        /// Create Single Delete, return object as response
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>
        /// Success: Delete object 
        /// Fail: Null object
        /// </returns>
        public Product Create(ProductRequestVM parameters)
        {
            Product product = _mapper.Map<Product>(parameters);
            product.UpdatedDate = DateTime.UtcNow;
            ///FIX: default update fk
            product.UpdatedBy = 1;
            _northwindContext.Products.Add(product);
            _northwindContext.SaveChanges();
            return product;
        }

        /// <summary>
        /// Get Product by productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetByID(int productId)
        {
            Product p = _northwindContext.Products
                .Where(p => p.Status == true && p.ProductId == productId)
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .First();
            return p;
        }

        public PagedResultsVM<Product> GetPaging(ProductPagingVM parameters)
        {
            /// TODO:
            /// 2. query by supplierName, categoryName
            var queryStatement = _northwindContext.Products
                .Include(c => c.Category)
                .Include(c => c.Supplier)
                .Where(p => (p.Status == true)
                    && (parameters.name == null || p.ProductName == parameters.name)
                    && (parameters.supplierID == null || p.SupplierId == parameters.supplierID)
                    && (parameters.categoryID == null || p.CategoryId == parameters.categoryID));
            int totalRecords = queryStatement.Count();
            int totalPages = Convert.ToInt32(Math.Ceiling(((double)totalRecords / (double)parameters.pageSize)));
            List<Product>? records = queryStatement
                .OrderBy(p => p.ProductId)
                .Skip((parameters.pageIndex - 1) * parameters.pageSize)
                .Take(parameters.pageSize)
                .ToList();
            records??= new List<Product>();
            return new PagedResultsVM<Product>(records, totalRecords, parameters.pageIndex, parameters.pageSize, totalPages);
        }

        /// <summary>
        /// Update parameters by @productId, set value by @parameters
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="parameters"></param>
        /// <returns>
        /// 
        /// </returns>
        public Product Update(int productId, ProductRequestVM parameters)
        {
            var product = _northwindContext.Products
                .Where(p => p.ProductId == productId && p.Status == true)
                .FirstOrDefault();

            if (product != null)
            {
                /// TODO: make this part simplify
                product.ProductName = parameters.ProductName;
                product.SupplierId = parameters.SupplierID;
                product.CategoryId = parameters.CategoryID;
                product.QuantityPerUnit = parameters.QuantityPerUnit;
                product.UnitPrice = parameters.UnitPrice;
                product.UnitsInStock = (short?)parameters.UnitsInStock;
                product.UnitsOnOrder = (short?)parameters.UnitsOnOrder;
                product.ReorderLevel = (short?)parameters.ReorderLevel;
                product.Discontinued = parameters.Discontinued != 0;
                product.UpdatedDate = DateTime.Now;
                /// FIX: default update fk
                product.UpdatedBy = 1;
                _northwindContext.SaveChanges();
                return product;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Delete by ProductId; but Simply set product.[Status] = 0
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>
        /// the number of rows being efficited, if return 0 means delete failed
        /// </returns>
        public bool Delete(int productId)
        {
            ///Mark Product.[Status] to false rather than delete row
            var product = _northwindContext.Products
                .SingleOrDefault(p => p.ProductId == productId && p.Status == true);
            if (product == null)
            {
                return false;
            }
            product.Status = false;
            product.UpdatedDate = DateTime.UtcNow;
            ///FIX: default update fk
            product.UpdatedBy = 1;
            _northwindContext.Products.Update(product);
            _northwindContext.SaveChanges();
            return true;
        }
    }
}
