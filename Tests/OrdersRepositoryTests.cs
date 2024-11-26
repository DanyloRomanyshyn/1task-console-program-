using System;
using System.Linq;
using DAL;
using Xunit;

public class OrdersRepositoryTests
{
    private readonly string _connectionString = "Host=localhost;Port=5432;Database=WarehouseDB;Username=postgres;Password=Wdhjkopz1!";

    [Fact]
    public void AddOrder_ShouldAddOrder()
    {
        var productRepo = new ProductsRepository(_connectionString);
        var supplierRepo = new SuppliersRepository(_connectionString);
        var orderRepo = new OrdersRepository(_connectionString);

        // Add necessary product and supplier
        productRepo.AddProduct("Test Product for Order", 50);
        var product = productRepo.GetAllProducts().LastOrDefault(p => p.Name == "Test Product for Order");
        Assert.NotNull(product); // Ensure product is added

        supplierRepo.AddSupplier("Test Supplier for Order", "test@supplier.com");
        var supplier = supplierRepo.GetAllSuppliers().LastOrDefault(s => s.Name == "Test Supplier for Order");
        Assert.NotNull(supplier); // Ensure supplier is added

        // Add order
        orderRepo.AddOrder(product.ProductId, supplier.SupplierId, 10, "Active");

        var orders = orderRepo.GetAllOrders().ToList();
        var addedOrder = orders.LastOrDefault(o => o.Quantity == 10);

        Assert.NotNull(addedOrder);
        Assert.Equal(product.ProductId, addedOrder.ProductId);
        Assert.Equal(supplier.SupplierId, addedOrder.SupplierId);
    }



    [Fact]
    public void GetAllOrders_ShouldReturnOrders()
    {
        var repository = new OrdersRepository(_connectionString);

        var orders = repository.GetAllOrders().ToList();

        Assert.True(orders.Count > 0, "No orders found.");
    }

    [Fact]
    public void UpdateOrder_ShouldUpdateOrder()
    {
        var productRepo = new ProductsRepository(_connectionString);
        var supplierRepo = new SuppliersRepository(_connectionString);
        var orderRepo = new OrdersRepository(_connectionString);

        // Add necessary product and supplier
        productRepo.AddProduct("Product for Update", 50);
        supplierRepo.AddSupplier("Supplier for Update", "update@supplier.com");

        var product = productRepo.GetAllProducts().Last();
        var supplier = supplierRepo.GetAllSuppliers().Last();

        // Add order
        orderRepo.AddOrder(product.ProductId, supplier.SupplierId, 10, "Active");
        var order = orderRepo.GetAllOrders().Last();

        // Update order
        orderRepo.UpdateOrder(order.OrderId, product.ProductId, supplier.SupplierId, 20, "Completed");

        var updatedOrder = orderRepo.GetAllOrders().FirstOrDefault(o => o.OrderId == order.OrderId);

        Assert.NotNull(updatedOrder);
        Assert.Equal("Completed", updatedOrder.Status);
    }

    [Fact]
    public void DeleteOrder_ShouldDeleteOrder()
    {
        var productRepo = new ProductsRepository(_connectionString);
        var supplierRepo = new SuppliersRepository(_connectionString);
        var orderRepo = new OrdersRepository(_connectionString);

        // Add necessary product and supplier
        productRepo.AddProduct("Product for Deletion", 50);
        supplierRepo.AddSupplier("Supplier for Deletion", "delete@supplier.com");

        var product = productRepo.GetAllProducts().Last();
        var supplier = supplierRepo.GetAllSuppliers().Last();

        // Add order
        orderRepo.AddOrder(product.ProductId, supplier.SupplierId, 10, "Active");
        var order = orderRepo.GetAllOrders().Last();

        // Delete order
        orderRepo.DeleteOrder(order.OrderId);

        var deletedOrder = orderRepo.GetAllOrders().FirstOrDefault(o => o.OrderId == order.OrderId);

        Assert.Null(deletedOrder);
    }
}
