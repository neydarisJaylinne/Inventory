using Inventory.Context;
using Inventory.Models;

namespace Inventory.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;

        public ProductRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> FilterByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public IEnumerable<Product> FilterByName(string name)
        {
            return _context.Products.Where(p => p.Name.Contains(name)).ToList();
        }

        public IEnumerable<Product> FilterByQuantity(int quantity)
        {
            return _context.Products.Where(p => p.Quantity >= quantity).ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product IncreaseProductQuantity(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                product.Quantity += quantity;
                _context.SaveChanges();
            }
            return product;
        }
    }
}
