using SimpleInventoryManagementSystem.Domain.InventoryManagement;
using SimpleInventoryManagementSystem.Domain.ProductManagement;

var inventory = new Inventory
{
    Products =
    [
        new Product("Laptop", 1500.00f, 10),
        new Product("Smartphone", 800.00f, 20),
        new Product("Tablet", 500.00f, 15)
    ]
};


while ( true )
{
    Menu();
    var option = Console.ReadLine();
    int i ;
    
    if ( option != null && int.TryParse( option, out i ) )
    {
        switch (i)
        {
            case 1:
                // add new product
                break;
            case 2:
                // view all products
                break;
            case 3:
                // edit a product
                break;
            case 4:
                // delete a product
                break;
            case 5:
                // search a product
                break;
            case 6:
                return;
            default:
                Console.WriteLine("Invalid Option !!! Try again.");
                break;
        }

    }
    else
    {
        Console.WriteLine("Invalid Option !!! Try again.");
    }
   
}






void Menu()
{
    Console.WriteLine("Enter your preferred option:\n" +
                      "1. Add new product.\n" +
                      "2. View all products.\n" +
                      "3. Edit a product.\n" +
                      "4. Delete a product.\n" +
                      "5. Search for a product.\n" +
                      "6. Exit.\n");
}