using Inventory.Context;
using Inventory.Models;
using Inventory.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HouseInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public InventoryController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("Products")]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("Categories")]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product productDto)
        {
            var category = _categoryRepository.GetAllCategories().FirstOrDefault(c => c.Id == productDto.CategoryId) ?? new Category { Name = productDto.Category?.Name };
            if (category.Id == 0)
            {
                _categoryRepository.AddCategory(category);
            }

            var addedProduct = _productRepository.AddProduct(new Product
            {
                Name = productDto.Name,
                Quantity = productDto.Quantity,
                CategoryId = category.Id,
                Category = category
            });

            return Ok(addedProduct);
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(Category category)
        {
            var addedCategory = _categoryRepository.AddCategory(category);
            return Ok(addedCategory);
        }

        [HttpDelete("DeleteProduct/{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            _productRepository.DeleteProduct(productId);
            return NoContent();
        }

        [HttpDelete("DeleteCategory/{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            _categoryRepository.DeleteCategory(categoryId);
            return NoContent();
        }

        [HttpPost("IncreaseProductQuantity/{productId}")]
        public IActionResult IncreaseProductQuantity(int productId, int quantity)
        {
            var product = _productRepository.GetAllProducts().FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            product.Quantity += quantity;
            _productRepository.AddProduct(product);
            return Ok(product);
        }

        [HttpGet("FilterByName/{name}")]
        public IActionResult FilterByName(string name)
        {
            var products = _productRepository.FilterByName(name);
            return Ok(products);
        }

        [HttpGet("FilterByCategory/{categoryId}")]
        public IActionResult FilterByCategory(int categoryId)
        {
            var products = _productRepository.FilterByCategory(categoryId);
            return Ok(products);
        }

        [HttpGet("FilterByQuantity/{quantity}")]
        public IActionResult FilterByQuantity(int quantity)
        {
            var products = _productRepository.FilterByQuantity(quantity);
            return Ok(products);
        }
    }
}


