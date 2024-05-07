using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SimpleInventoryManagementSystem.Domain.ProductManagement;

namespace SimpleInventoryManagementSystem.Domain.Database;

public class DatabaseService : IDatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public void ExecuteCommand(string commandText)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = new SqlCommand(commandText, connection);
        var data = command.ExecuteNonQuery();

        connection.Close();
    }

    public List<Product> GetQueryResults(string query)
    {
        var results = new List<Product>();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using (var command = new SqlCommand(query, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var product = new Product(
                            reader.GetString(1),
                            (float)reader.GetDouble(2),
                            reader.GetInt32(3)
                        );
                        results.Add(product);
                    }
                }
            }
        }

        connection.Close();

        return results;
    }
}
