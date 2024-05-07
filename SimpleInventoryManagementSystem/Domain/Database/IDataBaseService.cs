using System.Data.SqlClient;
using SimpleInventoryManagementSystem.Domain.ProductManagement;

namespace SimpleInventoryManagementSystem.Domain.Database;

public interface IDatabaseService
{
    public void ExecuteCommand(string commandText);
    public List<Product> GetQueryResults(string query);
}