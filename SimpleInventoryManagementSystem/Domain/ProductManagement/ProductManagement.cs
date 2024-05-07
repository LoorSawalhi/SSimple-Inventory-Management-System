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
        var products = _databaseService.GetQueryResults($"""
                                         SELECT *
                                         FROM product
                                         WHERE name = '{name}'
                                         """);

        return products[0];
    }

    public List<Product> AddNewProduct(Product product)
    {
        _databaseService.ExecuteCommand($"""
                                        INSERT INTO product 
                                        VALUES ('{product.Name}',{product.Price}, {product.Quantity})
                                        """);
        var products = _databaseService.GetQueryResults("SELECT * FROM product");
        return products;
    }

    public List<Product> DeleteProduct(Product product)
    {
        var tempProduct = FindProduct(product.Name);

        _databaseService.ExecuteCommand($"""
                                         DELETE FROM product 
                                         WHERE id = {tempProduct.Id}
                                         """);
        var products = _databaseService.GetQueryResults("SELECT * FROM product");
        return products;
    }
}