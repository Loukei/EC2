using NorthWindLibrary.DTOs;

namespace EC2.Repository
{
    public interface ICategoryRepository
    {
        Category GetCategoryByID(int categoryId);
    }
}
