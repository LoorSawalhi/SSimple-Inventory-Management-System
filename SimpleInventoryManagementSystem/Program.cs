using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using SimpleInventoryManagementSystem;
using SimpleInventoryManagementSystem.Domain.Database;
using SimpleInventoryManagementSystem.Domain.ProductManagement;

var builder = new ConfigurationBuilder()
    .SetBasePath("/home/loor/Desktop/Foothill Training/C#/SimpleInventoryManagementSystem/SimpleInventoryManagementSystem/")
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfiguration configuration = builder.Build();

var dataService = new DatabaseService(configuration);
var productManagement = new ProductManagement(dataService);
var utilities = new Utilities(productManagement);
utilities.Menu();

