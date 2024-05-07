using SimpleInventoryManagementSystem;
using Microsoft.Extensions.Configuration;
using SimpleInventoryManagementSystem.Domain.Database;
using SimpleInventoryManagementSystem.Domain.ProductManagement;

class Program
{
    static void Main()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath("/home/loor/Desktop/Foothill Training/C#/SimpleInventoryManagementSystem/SimpleInventoryManagementSystem/")
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfiguration configuration = builder.Build();

        var databaseService = new DatabaseService(configuration);
        var productManagement = new ProductManagement(databaseService);
        var utilities = new Utilities(productManagement);
        utilities.Menu();
    }
}