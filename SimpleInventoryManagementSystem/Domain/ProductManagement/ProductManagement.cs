using SimpleInventoryManagementSystem.Domain.Database;

namespace SimpleInventoryManagementSystem.Domain.ProductManagement;

public class ProductManagement
{
    private readonly IDatabaseService _databaseService;
    private List<Product> _products = [];

    public List<Product> Products
    {
        get => _products;
        set => _products = value;
    }

    public ProductManagement(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
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

    public List<Product> DeleteProduct(Product product)
    {
        Products.Remove(product);
        Console.WriteLine("Successfully deleted!");
        return Products;
    }
}