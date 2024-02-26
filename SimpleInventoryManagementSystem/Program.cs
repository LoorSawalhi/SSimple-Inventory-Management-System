using SimpleInventoryManagementSystem.Domain.InventoryManagement;
using SimpleInventoryManagementSystem.Domain.ProductManagement;

var inventory = new Inventory
{
    Products =
    [
        new Product("Laptop", 1500.00f, 10),
        new Product("Smartphone", 800.00f, 20),
        new Product("Tablet", 500.00f, 15)
    ]
};

// Display the inventory
foreach (var product in inventory.Products)
{
    System.Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
}
