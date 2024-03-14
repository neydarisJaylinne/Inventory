using Inventory.Context;
using Inventory.Models;

namespace Inventory.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventoryDbContext _context;


        public CategoryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public Category AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
