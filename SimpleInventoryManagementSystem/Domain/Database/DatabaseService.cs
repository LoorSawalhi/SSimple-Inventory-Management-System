using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleInventoryManagementSystem.Domain.ProductManagement;

namespace SimpleInventoryManagementSystem.Domain.Database;

public class DatabaseService
{
    public IMongoCollection<BsonDocument> Collection { get; }

    public DatabaseService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var client = new MongoClient(connectionString);

        Collection = client.GetDatabase("inventory-system").GetCollection<BsonDocument>("foothill_training");
    }

    public List<Product> ProductsList(IEnumerable<BsonDocument> documents)
    {
        return documents.Select(document => new Product(document["name"].AsString,
            (float)document["price"].ToDouble(), document["quantity"].AsInt32)).ToList();
    }
}
