namespace SimpleInventoryManagementSystem;
using Domain.InventoryManagement;
using Domain.ProductManagement;

public static class Utilities
{
    private static Inventory Inventory { get; set; } = new();

    internal static void InitializeInventory()
    {
        Inventory = new Inventory
        {
            Products =
            [
                new Product("Laptop", 1500.00f, 10),
                new Product("Smartphone", 800.00f, 20),
                new Product("Tablet", 500.00f, 15),
                new Product("Phone", 10.00f, 10),
                new Product("Bottle", 20.00f, 20),
                new Product("Screen", 30.00f, 15),
                new Product("Car", 1580.00f, 10),
                new Product("Door", 870.00f, 20),
                new Product("Glasses", 300.00f, 15)
            ]
        };
    }
    
    internal static void Menu()
    {
        Console.WriteLine("\nEnter your preferred option:\n" +
                          "1. Add new product.\n" +
                          "2. View all products.\n" +
                          "3. Edit a product.\n" +
                          "4. Delete a product.\n" +
                          "5. Search for a product.\n" +
                          "6. Exit.\n");
        
        Console.Write("Option = ");
        
        var option = Console.ReadLine();
        int i ;
    
        if ( option != null && int.TryParse( option, out i ) )
        {
            switch (i)
            {
                case 1:
                    DisplayAddNewProductMenu();
                    break;
                case 2:
                    DisplayAllProducts();
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
                    Menu();
                    break;
            }

        }
        else
        {
            Console.WriteLine("Invalid Option !!! Try again.");
        }
    }

    private static void DisplayAddNewProductMenu()
    {
        string name;
        
        do
        {
            Console.Write("Enter product name:");
            name = Console.ReadLine() ?? string.Empty;

            if (name.Length <= 0)
            {
                Console.WriteLine("Empty name field !! TRY AGAIN");
            }
        } 
        while (name.Length <= 0);

        
        var p = Inventory.FindProduct( name );
        
        if ( p != null )
        {
          Console.WriteLine( "Product name already exists !!" );  
          DisplayAddNewProductMenu();
        }
        else
        {
            var price = 0.0f;
            var quantity = 0;
            string input;

            do
            {
                Console.Write("Enter product price: ");
                input = Console.ReadLine() ?? string.Empty;

                if (input.Length <= 0)
                {
                    Console.WriteLine("Empty price field! TRY AGAIN");
                }
                else if ( !float.TryParse(input, out price) || price <= 0 )
                {
                    if ((int)price == -1)
                    {
                        Menu();
                    }
                    
                    Console.WriteLine("Invalid price! Price must be a number greater than 0. TRY AGAIN");
                    input = ""; 
                }
            } while (input.Length <= 0);

            input = "";

            do
            {
                Console.Write("Enter product quantity: ");
                input = Console.ReadLine() ?? string.Empty;

                if (input.Length <= 0)
                {
                    Console.WriteLine("Empty quantity field! TRY AGAIN");
                }
                else if (!int.TryParse(input, out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid quantity! Quantity must be a number greater than 0. TRY AGAIN");
                    input = ""; 
                }
            } while (input.Length <= 0);
            

            var product = new Product(name, price, quantity);
            Inventory.AddNewProduct(product);
            
            Console.WriteLine("Product Added.");
        }
        
        Menu();
    }
    
    private static void DisplayAllProducts()
    {
        var i = 1;
        Console.WriteLine("Inventory Products:");
        
        foreach (var product in Inventory.Products)
        {
            Console.WriteLine( i + ". " + product.ToString() );
            i += 1;
        }
        
        Menu();
    }
}