using Inventory.Models;

namespace Inventory.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product AddProduct(Product product);
        void DeleteProduct(int productId);
        Product IncreaseProductQuantity(int productId, int quantity);
        IEnumerable<Product> FilterByName(string name);
        IEnumerable<Product> FilterByCategory(int categoryId);
        IEnumerable<Product> FilterByQuantity(int quantity);
    
}
}
