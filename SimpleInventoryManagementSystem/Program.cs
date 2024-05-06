using SimpleInventoryManagementSystem;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SimpleInventoryManagementSystem.Domain.Database;
using SimpleInventoryManagementSystem.Domain.ProductManagement;

class Program
{
    private static IConfigurationRoot Configuration;

    static void Main()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath("/home/loor/Desktop/Foothill Training/C#/SimpleInventoryManagementSystem/SimpleInventoryManagementSystem/")
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfiguration configuration = builder.Build();

        // var connectionString = configuration.GetConnectionString("DefaultConnection");
        var databaseService = new DatabaseService(configuration);
        var productManagement = new ProductManagement(databaseService);
        var utilities = new Utilities(productManagement);
    }


    static void SetupConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        Configuration = builder.Build();
    }
}
// Utilities.InitializeInventory();
// Utilities.Menu();



