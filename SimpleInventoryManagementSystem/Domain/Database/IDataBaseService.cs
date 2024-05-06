namespace SimpleInventoryManagementSystem.Domain.Database;

public interface IDatabaseService
{
    public void ExecuteCommand(string commandText);
}