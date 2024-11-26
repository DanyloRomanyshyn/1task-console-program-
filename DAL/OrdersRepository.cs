using System;
using System.Collections.Generic;
using Npgsql;

namespace DAL
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }

    public class OrdersRepository
    {
        private readonly string _connectionString;

        public OrdersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Add a new order
        public void AddOrder(int productId, int supplierId, int quantity, string status = "Active")
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO orders (productid, supplierid, quantity, status) VALUES (@productId, @supplierId, @quantity, @status)";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("productId", productId);
                    command.Parameters.AddWithValue("supplierId", supplierId);
                    command.Parameters.AddWithValue("quantity", quantity);
                    command.Parameters.AddWithValue("status", status);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Get all orders
        public IEnumerable<Order> GetAllOrders()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM orders";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var orders = new List<Order>();
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                OrderId = reader.GetInt32(0),
                                ProductId = reader.GetInt32(1),
                                SupplierId = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                Status = reader.GetString(4)
                            });
                        }
                        return orders;
                    }
                }
            }
        }

        // Update an order
        public void UpdateOrder(int orderId, int productId, int supplierId, int quantity, string status)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE orders SET productid = @productId, supplierid = @supplierId, quantity = @quantity, status = @status WHERE orderid = @orderId";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("productId", productId);
                    command.Parameters.AddWithValue("supplierId", supplierId);
                    command.Parameters.AddWithValue("quantity", quantity);
                    command.Parameters.AddWithValue("status", status);
                    command.Parameters.AddWithValue("orderId", orderId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete an order
        public void DeleteOrder(int orderId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM orders WHERE orderid = @orderId";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("orderId", orderId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
