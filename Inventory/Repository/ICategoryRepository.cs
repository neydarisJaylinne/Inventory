using Inventory.Models;

namespace Inventory.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category AddCategory(Category category);
        void DeleteCategory(int categoryId);
    }
}
