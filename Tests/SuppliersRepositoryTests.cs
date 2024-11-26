using System;
using System.Linq;
using DAL;
using Xunit;

public class SuppliersRepositoryTests
{
    private readonly string _connectionString = "Host=localhost;Port=5432;Database=WarehouseDB;Username=postgres;Password=Wdhjkopz1!";

    [Fact]
    public void AddSupplier_ShouldAddSupplier()
    {
        var repository = new SuppliersRepository(_connectionString);

        repository.AddSupplier("Test Supplier", "test@supplier.com");

        var suppliers = repository.GetAllSuppliers().ToList();
        var addedSupplier = suppliers.LastOrDefault(s => s.Name == "Test Supplier");

        Assert.NotNull(addedSupplier);
        Assert.Equal("Test Supplier", addedSupplier.Name);
    }

    [Fact]
    public void GetAllSuppliers_ShouldReturnSuppliers()
    {
        var repository = new SuppliersRepository(_connectionString);

        var suppliers = repository.GetAllSuppliers().ToList();

        Assert.True(suppliers.Count > 0, "No suppliers found.");
    }

    [Fact]
    public void UpdateSupplier_ShouldUpdateSupplier()
    {
        var repository = new SuppliersRepository(_connectionString);

        repository.AddSupplier("Supplier to Update", "update@supplier.com");
        var supplier = repository.GetAllSuppliers().Last();
        repository.UpdateSupplier(supplier.SupplierId, "Updated Supplier", "updated@supplier.com");

        var updatedSupplier = repository.GetAllSuppliers().FirstOrDefault(s => s.SupplierId == supplier.SupplierId);

        Assert.NotNull(updatedSupplier);
        Assert.Equal("Updated Supplier", updatedSupplier.Name);
    }

    [Fact]
    public void DeleteSupplier_ShouldDeleteSupplier()
    {
        var repository = new SuppliersRepository(_connectionString);

        repository.AddSupplier("Supplier to Delete", "delete@supplier.com");
        var supplier = repository.GetAllSuppliers().Last();

        repository.DeleteSupplier(supplier.SupplierId);

        var deletedSupplier = repository.GetAllSuppliers().FirstOrDefault(s => s.SupplierId == supplier.SupplierId);

        Assert.Null(deletedSupplier);
    }
}
