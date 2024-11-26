using System;
using System.Linq;
using DAL;
using Xunit;

public class ProductsRepositoryTests
{
    private readonly string _connectionString = "Host=localhost;Port=5432;Database=WarehouseDB;Username=postgres;Password=Wdhjkopz1!";

    [Fact]
    public void AddProduct_ShouldAddProduct()
    {
        var repository = new ProductsRepository(_connectionString);

        repository.AddProduct("Test Product", 100);

        var products = repository.GetAllProducts().ToList();
        var addedProduct = products.LastOrDefault(p => p.Name == "Test Product");

        Assert.NotNull(addedProduct);
        Assert.Equal("Test Product", addedProduct.Name);
    }

    [Fact]
    public void GetAllProducts_ShouldReturnProducts()
    {
        var repository = new ProductsRepository(_connectionString);

        var products = repository.GetAllProducts().ToList();

        Assert.True(products.Count > 0, "No products found.");
    }

    [Fact]
    public void UpdateProduct_ShouldUpdateProduct()
    {
        var repository = new ProductsRepository(_connectionString);

        repository.AddProduct("Product to Update", 50);
        var product = repository.GetAllProducts().Last();
        repository.UpdateProduct(product.ProductId, "Updated Product", 200);

        var updatedProduct = repository.GetAllProducts().FirstOrDefault(p => p.ProductId == product.ProductId);

        Assert.NotNull(updatedProduct);
        Assert.Equal("Updated Product", updatedProduct.Name);
    }

    [Fact]
    public void DeleteProduct_ShouldDeleteProduct()
    {
        var repository = new ProductsRepository(_connectionString);

        repository.AddProduct("Product to Delete", 30);
        var product = repository.GetAllProducts().Last();

        repository.DeleteProduct(product.ProductId);

        var deletedProduct = repository.GetAllProducts().FirstOrDefault(p => p.ProductId == product.ProductId);

        Assert.Null(deletedProduct);
    }
}
