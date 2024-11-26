using System;
using System.Collections.Generic;
using System.Linq;
using DAL;

namespace ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Port=5432;Database=WarehouseDB;Username=postgres;Password=Wdhjkopz1!";

            ProductsRepository productsRepo = new ProductsRepository(connectionString);
            SuppliersRepository suppliersRepo = new SuppliersRepository(connectionString);
            OrdersRepository ordersRepo = new OrdersRepository(connectionString);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Management Menu ===");
                Console.WriteLine("1. Manage Products");
                Console.WriteLine("2. Manage Suppliers");
                Console.WriteLine("3. Manage Orders");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ManageProducts(productsRepo);
                            break;
                        case "2":
                            ManageSuppliers(suppliersRepo);
                            break;
                        case "3":
                            ManageOrders(ordersRepo);
                            break;
                        case "4":
                            Console.WriteLine("Exiting program...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }

        

        static void ManageSuppliers(SuppliersRepository suppliersRepo)
        {
            Console.Clear();
            Console.WriteLine("=== Supplier Management ===");
            Console.WriteLine("1. Add Supplier");
            Console.WriteLine("2. View Suppliers");
            Console.WriteLine("3. Update Supplier");
            Console.WriteLine("4. Delete Supplier");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    AddSupplier(suppliersRepo);
                    break;
                case "2":
                    ViewSuppliers(suppliersRepo);
                    break;
                case "3":
                    UpdateSupplier(suppliersRepo);
                    break;
                case "4":
                    DeleteSupplier(suppliersRepo);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void ManageProducts(ProductsRepository productsRepo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Product Management ===");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View Products");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Search Products");
                Console.WriteLine("6. Sort Products");
                Console.WriteLine("7. Back to Main Menu");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        AddProduct(productsRepo);
                        break;
                    case "2":
                        ViewProducts(productsRepo);
                        break;
                    case "3":
                        UpdateProduct(productsRepo);
                        break;
                    case "4":
                        DeleteProduct(productsRepo);
                        break;
                    case "5":
                        SearchProducts(productsRepo);
                        break;
                    case "6":
                        SortProducts(productsRepo);
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }

        static void AddSupplier(SuppliersRepository suppliersRepo)
        {
            Console.WriteLine("=== Add a Supplier ===");
            Console.Write("Enter supplier name: ");
            string name = Console.ReadLine();

            Console.Write("Enter contact info: ");
            string contactInfo = Console.ReadLine();

            suppliersRepo.AddSupplier(name, contactInfo);
            Console.WriteLine("Supplier added successfully!");
        }

        static void ViewSuppliers(SuppliersRepository suppliersRepo)
        {
            Console.WriteLine("=== View All Suppliers ===");
            var suppliers = suppliersRepo.GetAllSuppliers().ToList();

            if (suppliers.Count > 0)
            {
                foreach (var supplier in suppliers)
                {
                    Console.WriteLine($"ID: {supplier.SupplierId}, Name: {supplier.Name}, Contact Info: {supplier.ContactInfo}");
                }
            }
            else
            {
                Console.WriteLine("No suppliers found.");
            }
        }

        static void UpdateSupplier(SuppliersRepository suppliersRepo)
        {
            Console.WriteLine("=== Update a Supplier ===");
            Console.Write("Enter supplier ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int supplierId))
            {
                Console.Write("Enter new supplier name: ");
                string name = Console.ReadLine();

                Console.Write("Enter new contact info: ");
                string contactInfo = Console.ReadLine();

                suppliersRepo.UpdateSupplier(supplierId, name, contactInfo);
                Console.WriteLine("Supplier updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid supplier ID. Supplier not updated.");
            }
        }

        static void DeleteSupplier(SuppliersRepository suppliersRepo)
        {
            Console.WriteLine("=== Delete a Supplier ===");
            Console.Write("Enter supplier ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int supplierId))
            {
                suppliersRepo.DeleteSupplier(supplierId);
                Console.WriteLine("Supplier deleted successfully!");
            }
            else
            {
                Console.WriteLine("Invalid supplier ID. Supplier not deleted.");
            }
        }
        static void AddOrder(OrdersRepository ordersRepo)
        {
            Console.WriteLine("=== Add an Order ===");
            Console.Write("Enter product ID: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.Write("Enter supplier ID: ");
                if (int.TryParse(Console.ReadLine(), out int supplierId))
                {
                    Console.Write("Enter quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        ordersRepo.AddOrder(productId, supplierId, quantity);
                        Console.WriteLine("Order added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity. Order not added.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid supplier ID. Order not added.");
                }
            }
            else
            {
                Console.WriteLine("Invalid product ID. Order not added.");
            }
        }

        static void ViewOrders(OrdersRepository ordersRepo)
        {
            Console.WriteLine("=== View All Orders ===");
            var orders = ordersRepo.GetAllOrders().ToList();

            if (orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    Console.WriteLine($"ID: {order.OrderId}, Product ID: {order.ProductId}, Supplier ID: {order.SupplierId}, Quantity: {order.Quantity}, Status: {order.Status}");
                }
            }
            else
            {
                Console.WriteLine("No orders found.");
            }
        }

        static void UpdateOrder(OrdersRepository ordersRepo)
        {
            Console.WriteLine("=== Update an Order ===");
            Console.Write("Enter order ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.Write("Enter new product ID: ");
                if (int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.Write("Enter new supplier ID: ");
                    if (int.TryParse(Console.ReadLine(), out int supplierId))
                    {
                        Console.Write("Enter new quantity: ");
                        if (int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.Write("Enter new status: ");
                            string status = Console.ReadLine();

                            ordersRepo.UpdateOrder(orderId, productId, supplierId, quantity, status);
                            Console.WriteLine("Order updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid quantity. Order not updated.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid supplier ID. Order not updated.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid product ID. Order not updated.");
                }
            }
            else
            {
                Console.WriteLine("Invalid order ID. Order not updated.");
            }
        }

        static void DeleteOrder(OrdersRepository ordersRepo)
        {
            Console.WriteLine("=== Delete an Order ===");
            Console.Write("Enter order ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                ordersRepo.DeleteOrder(orderId);
                Console.WriteLine("Order deleted successfully!");
            }
            else
            {
                Console.WriteLine("Invalid order ID. Order not deleted.");
            }
        }
        static void AddProduct(ProductsRepository productsRepo)
        {
            Console.WriteLine("=== Add a Product ===");
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter quantity: ");
            if (int.TryParse(Console.ReadLine(), out int quantity))
            {
                productsRepo.AddProduct(name, quantity);
                Console.WriteLine("Product added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid quantity. Product not added.");
            }
        }

        static void ViewProducts(ProductsRepository productsRepo)
        {
            Console.WriteLine("=== View All Products ===");
            var products = productsRepo.GetAllProducts().ToList();

            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Quantity: {product.Quantity}");
                }
            }
            else
            {
                Console.WriteLine("No products found.");
            }
        }

        static void UpdateProduct(ProductsRepository productsRepo)
        {
            Console.WriteLine("=== Update a Product ===");
            Console.Write("Enter product ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.Write("Enter new product name: ");
                string name = Console.ReadLine();

                Console.Write("Enter new quantity: ");
                if (int.TryParse(Console.ReadLine(), out int quantity))
                {
                    productsRepo.UpdateProduct(productId, name, quantity);
                    Console.WriteLine("Product updated successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid quantity. Product not updated.");
                }
            }
            else
            {
                Console.WriteLine("Invalid product ID. Product not updated.");
            }
        }

        static void DeleteProduct(ProductsRepository productsRepo)
        {
            Console.WriteLine("=== Delete a Product ===");
            Console.Write("Enter product ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                productsRepo.DeleteProduct(productId);
                Console.WriteLine("Product deleted successfully!");
            }
            else
            {
                Console.WriteLine("Invalid product ID. Product not deleted.");
            }
        }
        static void SearchProducts(ProductsRepository productsRepo)
        {
            Console.WriteLine("=== Search Products ===");
            Console.Write("Enter product name to search: ");
            string searchQuery = Console.ReadLine();

            var products = productsRepo.GetAllProducts()
            .Where(p => p.Name.ToLower().Contains(searchQuery.ToLower()))
            .ToList();



            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Quantity: {product.Quantity}");
                }
            }
            else
            {
                Console.WriteLine("No products found matching your search.");
            }
        }
        static void SortProducts(ProductsRepository productsRepo)
        {
            Console.WriteLine("=== Sort Products ===");
            Console.WriteLine("1. Sort by Name");
            Console.WriteLine("2. Sort by Quantity");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            var products = productsRepo.GetAllProducts().ToList();

            switch (choice)
            {
                case "1":
                    products = products.OrderBy(p => p.Name).ToList();
                    Console.WriteLine("\nProducts sorted by Name:");
                    break;
                case "2":
                    products = products.OrderBy(p => p.Quantity).ToList();
                    Console.WriteLine("\nProducts sorted by Quantity:");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Returning to menu.");
                    return;
            }

            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Quantity: {product.Quantity}");
            }
        }
        static void ManageOrders(OrdersRepository ordersRepo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Order Management ===");
                Console.WriteLine("1. Add Order");
                Console.WriteLine("2. View Orders");
                Console.WriteLine("3. Update Order");
                Console.WriteLine("4. Delete Order");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        AddOrder(ordersRepo);
                        break;
                    case "2":
                        ViewOrders(ordersRepo);
                        break;
                    case "3":
                        UpdateOrder(ordersRepo);
                        break;
                    case "4":
                        DeleteOrder(ordersRepo);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }

        // CRUD methods for Products, Suppliers, and Orders are similar to the previous implementation.
        // AddProduct, ViewProducts, UpdateProduct, DeleteProduct, etc., are implemented accordingly.
    }
}
