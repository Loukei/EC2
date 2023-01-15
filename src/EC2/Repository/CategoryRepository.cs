using EC2.Context;
using EC2.Models;
using Dapper;

namespace EC2.Repository
{
    public interface ICategoryRepository
    {
        Category GetCategoryByID(int categoryId);
    }


    public class CategoryRepository: ICategoryRepository
    {
        private readonly DapperContext _dapperContext;

        public CategoryRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Category GetCategoryByID(int categoryId)
        {
            using (var conn = _dapperContext.CreateConnection())
            {
                string sql = @"
                    SELECT [CategoryID] ,[CategoryName],[Description] 
                    FROM Categories 
                    WHERE [CategoryID] = @categoryId";
                var result = conn.QuerySingleOrDefault<Category>(sql, new { categoryId = categoryId });
                return result;
            }
        }
    }
}
