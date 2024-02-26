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


    public Product? FindProduct(string name)
    {
        name = name.ToLower();
        return Products.FirstOrDefault(product => product.Name.ToLower().Equals(name));
    }

    public List<Product> AddNewProduct(Product product)
    {
        Products.Add(product);
        return Products;
    }
    
}