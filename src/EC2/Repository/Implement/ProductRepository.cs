using Microsoft.EntityFrameworkCore;
using AutoMapper;
using X.PagedList;
using EC2.Models;
using NorthWindLibrary.Context;
using NorthWindLibrary.DTOs;

namespace EC2.Repository.Implement
{
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
        /// Turn <paramref name="parameters"/> to query Statement.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private IOrderedQueryable<Product> GetQuaryStatement(ProductPageQueryVM parameters)
        {
            /// TODO: query by supplierName, categoryName
            ///
            var records = from p in _northwindContext.Products
                          where (p.Status == true)
                            && (parameters.name == null || p.ProductName == parameters.name)
                            && (parameters.supplierID == null || p.SupplierId == parameters.supplierID)
                            && (parameters.categoryID == null || p.CategoryId == parameters.categoryID)
                          orderby p.ProductId ascending
                          select p;
            return records;
        }

        /// <summary>
        /// Create Single Delete, return object as response
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>
        /// Success: Delete object 
        /// Fail: Null object
        /// </returns>
        public Product Create(ProductUpdateVM parameters)
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

        public IPagedList<Product> GetPaging(ProductPageQueryVM parameters)
        {
            var records = GetQuaryStatement(parameters);
            return records.ToPagedList(parameters.pageIndex, parameters.pageSize);
        }

        public int CountByQuery(ProductPageQueryVM parameters)
        {
            //var queryStatement = _northwindContext.Products
            //    .Include(c => c.Category)
            //    .Include(c => c.Supplier)
            //    .Where(p => p.Status == true
            //        && (parameters.name == null || p.ProductName == parameters.name)
            //        && (parameters.supplierID == null || p.SupplierId == parameters.supplierID)
            //        && (parameters.categoryID == null || p.CategoryId == parameters.categoryID));
            var queryStatement = GetQuaryStatement(parameters);
            return queryStatement.Count();
        }

        /// <summary>
        /// Update parameters by <paramref name="productId"/>, set value by <paramref name="parameters"/>
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="parameters"></param>
        /// <returns>
        /// 
        /// </returns>
        public Product Update(int productId, ProductUpdateVM parameters)
        {
            var product = _northwindContext.Products
                .Where(p => p.ProductId == productId && p.Status == true)
                .FirstOrDefault();

            if (product != null)
            {
                _mapper.Map<ProductUpdateVM, Product>(parameters, product);
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
                .SingleOrDefault(p => p.ProductId == productId 
                && p.Status == true);
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
