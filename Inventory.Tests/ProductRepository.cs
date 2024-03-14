using HouseInventory.Controllers;
using Inventory.Models;
using Inventory.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Inventory.Tests;

public class ProductRepository
{
    [Fact]
    public void GetAllProducts_ReturnsAllProducts()
    {
        // Arrange
        var mockProductRepository = new Mock<IProductRepository>();
        var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Quantity = 10, CategoryId = 1 },
                new Product { Id = 2, Name = "Product 2", Quantity = 15, CategoryId = 2 }
            };
        mockProductRepository.Setup(repo => repo.GetAllProducts()).Returns(products);
        var controller = new InventoryController(mockProductRepository.Object, null);

        // Act
        var result = controller.GetAllProducts() as OkObjectResult;

        // Assert
        var returnedProducts = Assert.IsType<List<Product>>(result.Value);
        Assert.Equal(products.Count, returnedProducts.Count);
        Assert.Equal(products.First().Name, returnedProducts.First().Name);
    }

    [Fact]
    public void AddProduct_ReturnsAddedProduct()
    {
        // Arrange
        var mockProductRepository = new Mock<IProductRepository>();
        var mockCategoryRepository = new Mock<ICategoryRepository>();
        var productToAdd = new Product { Id = 1, Name = "New Product", Quantity = 5, CategoryId = 1 };
        mockCategoryRepository.Setup(repo => repo.GetAllCategories()).Returns(new List<Category>());
        mockProductRepository.Setup(repo => repo.AddProduct(It.IsAny<Product>())).Returns(productToAdd);
        var controller = new InventoryController(mockProductRepository.Object, mockCategoryRepository.Object);

        // Act
        var result = controller.AddProduct(productToAdd) as OkObjectResult;

        // Assert
        var addedProduct = Assert.IsType<Product>(result.Value);
        Assert.Equal(productToAdd.Name, addedProduct.Name);
    }

}