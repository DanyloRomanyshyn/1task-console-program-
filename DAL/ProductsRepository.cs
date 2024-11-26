using System;
using System.Collections.Generic;
using Npgsql;

namespace DAL
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductsRepository
    {
        private readonly string _connectionString;

        public ProductsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Create a new product
        public void AddProduct(string name, int quantity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO products (name, quantity) VALUES (@name, @quantity)";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("quantity", quantity);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Get all products
        public IEnumerable<Product> GetAllProducts()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM products";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var products = new List<Product>();
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Quantity = reader.GetInt32(2)
                            });
                        }
                        return products;
                    }
                }
            }
        }

        // Update a product
        public void UpdateProduct(int productId, string name, int quantity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE products SET name = @name, quantity = @quantity WHERE productid = @productId";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("quantity", quantity);
                    command.Parameters.AddWithValue("productId", productId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a product
        public void DeleteProduct(int productId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM products WHERE productid = @productId";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("productId", productId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
