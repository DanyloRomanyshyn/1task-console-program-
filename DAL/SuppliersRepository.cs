using System;
using System.Collections.Generic;
using Npgsql;

namespace DAL
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
    }

    public class SuppliersRepository
    {
        private readonly string _connectionString;

        public SuppliersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Add a new supplier
        public void AddSupplier(string name, string contactInfo)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO suppliers (name, contactinfo) VALUES (@name, @contactInfo)";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("contactInfo", contactInfo);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Get all suppliers
        public IEnumerable<Supplier> GetAllSuppliers()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM suppliers";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var suppliers = new List<Supplier>();
                        while (reader.Read())
                        {
                            suppliers.Add(new Supplier
                            {
                                SupplierId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ContactInfo = reader.GetString(2)
                            });
                        }
                        return suppliers;
                    }
                }
            }
        }

        // Update a supplier
        public void UpdateSupplier(int supplierId, string name, string contactInfo)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE suppliers SET name = @name, contactinfo = @contactInfo WHERE supplierid = @supplierId";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("contactInfo", contactInfo);
                    command.Parameters.AddWithValue("supplierId", supplierId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a supplier
        public void DeleteSupplier(int supplierId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM suppliers WHERE supplierid = @supplierId";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("supplierId", supplierId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
