using EC2.Models;
using EC2.Models.EFCore;
using EC2.Models.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace EC2.Repository
{
    public interface IProductRepository {      
        Product Create(ProductViewModel product);
        Product GetByID(int productId);
        IEnumerable<Product> GetAll(string? name, int? supplierID, int? categoryID, int pageIndex, int pageSize);
        ProductPagingResponseModel GetPaging(string? name, int? supplierID, int? categoryID, int pageIndex, int pageSize);
        Product Update(int productId, ProductViewModel product);        
        bool Delete(int productId);
    }

    public class ProductRepository: IProductRepository
    {
        private readonly NorthwindContext _northwindContext;
        public ProductRepository(NorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        /// <summary>
        /// Turn ProductViewModel to Product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Product ModelToProduct(ProductViewModel model)
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

        private static void fillingProduct(ProductViewModel model, ref Product output)
        {
            output.ProductName = model.ProductName;
            output.SupplierId = model.SupplierID;
            output.CategoryId = model.CategoryID;
            output.QuantityPerUnit = model.QuantityPerUnit;
            output.UnitPrice = model.UnitPrice;
            output.UnitsInStock = (short?)model.UnitsInStock;
            output.UnitsOnOrder = (short?)model.UnitsOnOrder;
            output.ReorderLevel = (short?)model.ReorderLevel;
            output.Discontinued = model.Discontinued != 0;
        }

        /// <summary>
        /// Create Single Delete, return object as response
        /// </summary>
        /// <param name="product"></param>
        /// <returns>
        /// Success: Delete object 
        /// Fail: Null object
        /// </returns>
        public Product Create(ProductViewModel product)
        {
            var p = ModelToProduct(product);
            _northwindContext.Products.Add(p);
            _northwindContext.SaveChanges();
            return p;
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
                .First();
            return p;
        }

        /// <summary>
        /// Query Products by parameters
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// </returns>
        public IEnumerable<Product> GetAll(string? name, int? supplierID, int? categoryID, int pageIndex, int pageSize)
        {
            /// TODO: 解決循環引用的問題: product > supplier > product
            IEnumerable<Product> products = _northwindContext.Products
                .Where(p => p.Status == true
                    && (name == null || p.ProductName == name)
                    && (supplierID == null || p.SupplierId == supplierID)
                    && (categoryID == null || p.CategoryId == categoryID)
                )
                //select()
                .OrderBy(p => p.ProductId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return products;
        }

        public ProductPagingResponseModel GetPaging(string? name, int? supplierID, int? categoryID, int pageIndex, int pageSize)
        {
            /// TODO: 分頁查詢，並且返回包裝的ProductPagingResponseModel
            //using (var conn = _dapperContext.CreateConnection())
            //{
            //    /// 查詢2次
            //    string whereStatementSQL = @"
            //            ([SupplierID] = @SupplierID OR @SupplierID IS NULL)
            //            AND
            //            ([CategoryID] = @CategoryID OR @CategoryID IS NULL)
            //            AND
            //            ([ProductName] LIKE @name OR @name IS NULL)
            //            AND
            //            ([Status] = 1)";
            //    /// 1. 共有幾筆資料? 並計算出共有幾頁
            //    string countTotalRecordsSQL = @$"
            //            SELECT COUNT(1) FROM Products 
            //            WHERE {whereStatementSQL}";
            //    /// 2. 該頁的所有row
            //    string getTotalRecordsSQL = @$"
            //            SELECT * FROM Products 
            //            WHERE {whereStatementSQL}
            //            ORDER BY [PRODUCTID] DESC
            //            OFFSET (@pageIndex-1)*@pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;";

            //    int totalRecords = conn.QuerySingle<int>(countTotalRecordsSQL, 
            //        new 
            //        {
            //            CategoryID = categoryID,
            //            SupplierID = supplierID,
            //            name = $"%{name}%"
            //        });

            //    int totalPages = Convert.ToInt32(Math.Ceiling(((double)totalRecords / (double)pageSize)));
            //    IEnumerable<Product> data = conn.Query<Product>(getTotalRecordsSQL,
            //        new
            //        {
            //            CategoryID = categoryID,
            //            SupplierID = supplierID,
            //            name = $"%{name}%",
            //            pageSize = pageSize,
            //            pageIndex = pageIndex                 
            //        });
            //    return new ProductPagingResponseModel(data,totalRecords, pageIndex, pageSize,totalPages);
            //}
            return null;
        }

        /// <summary>
        /// Update parameters by @productId, set value by @parameters
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="parameters"></param>
        /// <returns>
        /// 
        /// </returns>
        public Product Update(int productId, ProductViewModel parameters)
        {            
            var product = _northwindContext.Products
                .Where(p => p.ProductId == productId && p.Status == true)
                .FirstOrDefault();

            if (product != null)
            {
                product.ProductName = parameters.ProductName;
                product.SupplierId = parameters.SupplierID;
                product.CategoryId = parameters.CategoryID;
                product.QuantityPerUnit = parameters.QuantityPerUnit;
                product.UnitPrice = parameters.UnitPrice;
                product.UnitsInStock = (short?)parameters.UnitsInStock;
                product.UnitsOnOrder = (short?)parameters.UnitsOnOrder;
                product.ReorderLevel = (short?)parameters.ReorderLevel;
                product.Discontinued = parameters.Discontinued != 0;
                _northwindContext.SaveChanges();
                return product;
            }
            else
            {
                return null;
            }           
        }

        /// <summary>
        /// Delete by ProductId; but Simply set p.[Status] = 0
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>
        /// the number of rows being efficited, if return 0 means delete failed
        /// </returns>
        public bool Delete(int productId)
        {
            ///Mark Product.[Status] to false rather than delete row
            var product = _northwindContext.Products
                .Single(p => p.ProductId == productId && p.Status == false);
            _northwindContext.Products.Add(product);
            _northwindContext.SaveChanges();
            ///TODO:
            /// change tracking
            /// [變更偵測和通知 - EF Core | Microsoft Learn](https://learn.microsoft.com/zh-tw/ef/core/change-tracking/change-detection)
            return true;
        }
    }
}
