using NorthWindLibrary.Context;
using NorthWindLibrary.DTOs;

namespace EC2.Repository.Implement
{
    public class CategoryRepository : ICategoryRepository
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
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
