using SimpleInventoryManagementSystem.Domain.ProductManagement;
namespace SimpleInventoryManagementSystem.Domain.InventoryManagement;

public class Inventory
{
    private List<Product> _products = [];


    public List<Product> Products
    {
        get => _products;
        set => _products = value;
    }
}