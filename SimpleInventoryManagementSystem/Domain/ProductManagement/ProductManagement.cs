using MongoDB.Bson;
using MongoDB.Driver;
using SimpleInventoryManagementSystem.Domain.Database;

namespace SimpleInventoryManagementSystem.Domain.ProductManagement;

public class ProductManagement
{
    private readonly DatabaseService _databaseService;
    private List<Product> _products = [];

    public List<Product> Products
    {
        get => _products;
        set => _products = value;
    }

    public ProductManagement(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public Product? FindProduct(string name)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("name", name);
        var document = _databaseService.Collection.Find(filter).ToList();
        var products = _databaseService.ProductsList(document);
        return products.Count == 0 ? null : products[0];
    }

    public List<Product> AddNewProduct(Product product)
    {
        var doc = new BsonDocument
        {
            { "name", product.Name },
            { "price", product.Price },
            { "quantity", product.Quantity }
        };
        _databaseService.Collection.InsertOne(doc);
        return AllProducts();
    }

    public List<Product> AllProducts()
    {
        var products = _databaseService.Collection.Find(new BsonDocument()).ToList();
        return _databaseService.ProductsList(products);
    }

    public List<Product> DeleteProduct(Product product)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("name", product.Name);
        _databaseService.Collection.DeleteOne(filter);

        var products = _databaseService.Collection.Find(new BsonDocument()).ToList();
        return _databaseService.ProductsList(products);
    }
}