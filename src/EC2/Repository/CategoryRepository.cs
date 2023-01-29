using EC2.Context;
using EC2.Models.DTOs.Northwind;

namespace EC2.Repository
{
    public interface ICategoryRepository
    {
        Category GetCategoryByID(int categoryId);
    }

    public class CategoryRepository: ICategoryRepository
    {
        private readonly NorthwindContext _northwindContext;

        public CategoryRepository(NorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Category GetCategoryByID(int categoryId)
        {
            try
            {
                return _northwindContext.Categories.Single(c => c.CategoryId == categoryId);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
