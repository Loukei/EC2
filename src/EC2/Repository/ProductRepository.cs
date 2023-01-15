using EC2.Context;
using EC2.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        private readonly DapperContext _dapperContext;
        public ProductRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
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
            /// Create Object and check FK
            string createSQL = @"
                INSERT INTO Products 
                (
                    [ProductName], 
                    [SupplierID], 
                    [CategoryID], 
                    [QuantityPerUnit], 
                    [UnitPrice], 
                    [UnitsInStock],
                    [UnitsOnOrder],
                    [ReorderLevel],
                    [Discontinued],
                    [UpdatedBy],
                    [UpdatedDate]
                )
                OUTPUT [INSERTED].*
                VALUES ( 
                    @ProductName,
                    (SELECT [SupplierID] FROM Suppliers WHERE [SupplierID] = @SupplierID), 
                    (SELECT [CategoryID] FROM Categories WHERE [CategoryID] = @CategoryID),
                    @QuantityPerUnit, 
                    @UnitPrice, 
                    @UnitsInStock,
                    @UnitsOnOrder,
                    @ReorderLevel,
                    @Discontinued,
                    @UpdatedBy,
                    getDate()
                );";
            using (var conn = _dapperContext.CreateConnection())
            {
                Object obj = new 
                {
                    ProductName = product.ProductName,
                    SupplierID = product.SupplierID,
                    CategoryID = product.CategoryID,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder,
                    ReorderLevel = product.ReorderLevel,
                    Discontinued = product.Discontinued,
                    /// Set UpdateBy admin user
                    UpdatedBy = 1,
                    //UpdatedDate = product.UpdatedDate
                };
                return conn.QuerySingleOrDefault<Product>(createSQL, obj);
            }
        }

        /// <summary>
        /// Get Product by productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetByID(int productId)
        {
            using (var conn = _dapperContext.CreateConnection())
            {
                string sql = @"SELECT * FROM Products WHERE [ProductID] = @productId AND [Status] = 1";             
                Product product = conn.QuerySingleOrDefault<Product>(sql, new { productId = productId });
                return product;
            }
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
            using (var conn = _dapperContext.CreateConnection())
            {
                string querystr = @"
                    SELECT * FROM Products 
                    WHERE 
                        ([SupplierID] = @SupplierID OR @SupplierID IS NULL)
                        AND
                        ([CategoryID] = @CategoryID OR @CategoryID IS NULL)
                        AND 
                        ([ProductName] LIKE @name OR @name IS NULL)
                        AND
                        ([Status] = 1)
                    ORDER BY [PRODUCTID] DESC
                    OFFSET (@pageIndex-1)*@pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;
                ";
                //if (categoryID.HasValue && categoryID.Value > 0)
                //    updateSQL += "where categoryID = @categoryID";
                var result = conn.Query<Product>(querystr,
                    new
                    {
                        CategoryID = categoryID,
                        SupplierID = supplierID,
                        name = $"%{name}%",
                        pageSize = pageSize,
                        pageIndex = pageIndex
                    });
                return result;
            }
        }

        public ProductPagingResponseModel GetPaging(string? name, int? supplierID, int? categoryID, int pageIndex, int pageSize)
        {
            /// TODO: 分頁查詢，並且返回包裝的ProductPagingResponseModel
            using (var conn = _dapperContext.CreateConnection())
            {
                /// 查詢2次
                string whereStatementSQL = @"
                        ([SupplierID] = @SupplierID OR @SupplierID IS NULL)
                        AND
                        ([CategoryID] = @CategoryID OR @CategoryID IS NULL)
                        AND
                        ([ProductName] LIKE @name OR @name IS NULL)
                        AND
                        ([Status] = 1)";
                /// 1. 共有幾筆資料? 並計算出共有幾頁
                string countTotalRecordsSQL = @$"
                        SELECT COUNT(1) FROM Products 
                        WHERE {whereStatementSQL}";
                /// 2. 該頁的所有row
                string getTotalRecordsSQL = @$"
                        SELECT * FROM Products 
                        WHERE {whereStatementSQL}
                        ORDER BY [PRODUCTID] DESC
                        OFFSET (@pageIndex-1)*@pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;";

                int totalRecords = conn.QuerySingle<int>(countTotalRecordsSQL, 
                    new 
                    {
                        CategoryID = categoryID,
                        SupplierID = supplierID,
                        name = $"%{name}%"
                    });
                
                int totalPages = Convert.ToInt32(Math.Ceiling(((double)totalRecords / (double)pageSize)));
                IEnumerable<Product> data = conn.Query<Product>(getTotalRecordsSQL,
                    new
                    {
                        CategoryID = categoryID,
                        SupplierID = supplierID,
                        name = $"%{name}%",
                        pageSize = pageSize,
                        pageIndex = pageIndex                 
                    });
                return new ProductPagingResponseModel(data,totalRecords, pageIndex, pageSize,totalPages);
            }
        }

        /// <summary>
        /// Update product by @productId, set value by @product
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="product"></param>
        /// <returns>
        /// 
        /// </returns>
        public Product Update(int productId, ProductViewModel product)
        {
            using(var conn = _dapperContext.CreateConnection())
            {
                string updateSQL = @"
                    UPDATE Products 
                    SET 
                        [ProductName] = @ProductName
                        ,[SupplierID] = @SupplierID
                        ,[CategoryID] = @CategoryID
                        ,[QuantityPerUnit] = @QuantityPerUnit
                        ,[UnitPrice] = @UnitPrice
                        ,[UnitsInStock] = @UnitsInStock
                        ,[UnitsOnOrder] = @UnitsOnOrder
                        ,[ReorderLevel] = @ReorderLevel
                        ,[Discontinued] = @Discontinued
                        ,[UpdatedDate] = GETDATE()
                        ,[UpdatedBy] = @UpdatedBy
                    OUTPUT [INSERTED].*
                    WHERE 
                        [ProductID] = @productID;                    
                ";
                Object obj = new
                {
                    ProductName = product.ProductName,
                    SupplierID = product.SupplierID,
                    CategoryID = product.CategoryID,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder,
                    ReorderLevel = product.ReorderLevel,
                    Discontinued = product.Discontinued,
                    /// set UpdatedBy to admin
                    UpdatedBy = 1,
                    productID = productId,
                };
                Product result = conn.QuerySingleOrDefault<Product>(updateSQL, obj);
                return result;
            }
        }

        /// <summary>
        /// Delete Delete by ProductId, but Simply set p.[Status] = 0
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>
        /// the number of rows being efficited, if return 0 means delete failed
        /// </returns>
        public bool Delete(int productId)
        {
            using (var conn = _dapperContext.CreateConnection())
            {
                string deleteSQL = @"
                    UPDATE [Products]
                    SET [Status] = 0
                    WHERE 
                        ([ProductID] = @productId)
                        AND
                        ([Status] = 1)
                    ";
                var effectedrow = conn.Execute(deleteSQL, new { productId = productId });
                return effectedrow > 0;
            }
        }
    }
}
