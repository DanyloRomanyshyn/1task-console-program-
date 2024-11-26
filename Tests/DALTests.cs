using System;
using DAL;

namespace Tests
{
    public class DALTests
    {
        public static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Port=5432;Database=WarehouseDB;Username=postgres;Password=Wdhjkopz1";

            ProductsRepository repository = new ProductsRepository(connectionString);

            // Add a product
            repository.AddProduct("Test Product", 50);
            Console.WriteLine("Product added successfully!");

            // Get all products
            var products = repository.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Quantity: {product.Quantity}");
            }

            // Update a product
            repository.UpdateProduct(1, "Updated Product", 100);
            Console.WriteLine("Product updated successfully!");

            // Delete a product
            repository.DeleteProduct(1);
            Console.WriteLine("Product deleted successfully!");
        }
    }
}
