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
        while (true)
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

            if (option != null && int.TryParse(option, out var i))
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
                        DisplayEditProductList();
                        break;
                    case 4:
                        DisplayDeleteProduct();
                        break;
                    case 5:
                        DisplaySearchProduct();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid Option !!! Try again.");
                        continue;
                }
            }
            else
            {
                Console.WriteLine("Invalid Option !!! Try again.");
                continue;
            }

            break;
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
    
    private static void DisplayEditProductList()
    {
        while (true)
        {
            string name;
            Product? p;

            do
            {
                Console.WriteLine();
                Console.Write("Enter product name : ");
                name = Console.ReadLine() ?? string.Empty;

                if (name.Length <= 0)
                {
                    Console.WriteLine("Empty name field !! TRY AGAIN");
                }

                p = Inventory.FindProduct(name);

                if (p == null)
                {
                    name = "";
                    Console.WriteLine("Product name doesn't exists !! TRY AGAIN");
                }
            } while (name.Length <= 0);

            Console.WriteLine("\nWhich field would you like to edit : \n" + "1. Name\n2. Price\n3. Quantity\n4.Exit\n");
            Console.Write("Option = ");
            
            var option = Console.ReadLine();
            
            Console.WriteLine();

            if (option != null && int.TryParse(option, out var i))
            {
                switch (i)
                {
                    case < 4 and > 0:
                        if (p != null) EditProduct(i, p);
                        break;
                    case 4:
                        Menu();
                        break;
                    default:
                        Console.WriteLine("Invalid Option !!! Try again.");
                        continue;
                }
            }
            else
            {
                Console.WriteLine("Invalid Option !!! Try again.");
                continue;
            }

            break;
        }
    }

    private static void EditProduct(int i, Product p)
    {
        var input = "";
        switch (i)
        {
            case 1:
            {
                do
                {
                    Console.Write("Enter New Name : ");
                    input = Console.ReadLine() ?? string.Empty;
                    
                    Console.WriteLine();
                    
                    if (input.Length <= 0)
                    {
                        Console.WriteLine("Empty name field! TRY AGAIN");
                    }
                } while ( input.Length <= 0 );

                if (input.ToLower().Trim().Equals(p.Name))
                {
                    Console.WriteLine( "You entered the same name, nothing have changed!" );
                } 
                else
                {
                    p.Name = input;
                    Console.WriteLine("Name Updated!");
                }
                
                Console.WriteLine(p.ToString());
                break;
            }
            case 2:
            {
                var price = 0.0f;
                
                do
                {
                    Console.Write("Enter new price : ");
                    input = Console.ReadLine() ?? string.Empty;
                    
                    Console.WriteLine();

                    if (input.Length <= 0)
                    {
                        Console.WriteLine("Empty price field! TRY AGAIN");
                    }
                    else if ( !float.TryParse(input, out price) || price <= 0 )
                    {
                        Console.WriteLine("Invalid price! Price must be a number greater than 0. TRY AGAIN");
                        input = ""; 
                    }
                } while (input.Length <= 0);

                if (Math.Abs(price - p.Price) < 0.00001)
                {
                    Console.WriteLine( "You entered the same price, nothing have changed!" );
                }
                else
                {
                    p.Price = price;
                    Console.WriteLine("Price Updated!");
                }
                
                Console.WriteLine(p.ToString());
                break;
            }
            case 3:
            {
                var quantity = 0;
                
                do
                {
                    Console.Write("Enter new quantity : ");
                    input = Console.ReadLine() ?? string.Empty;
                    
                    Console.WriteLine();
                    
                    if (input.Length <= 0)
                    {
                        Console.WriteLine("Empty quantity field! TRY AGAIN");
                    }
                    else if ( !int.TryParse(input, out quantity) || quantity <= 0 )
                    {
                        Console.WriteLine("Invalid quantity! Quantity must be a number greater than 0. TRY AGAIN");
                        input = ""; 
                    }
                } while (input.Length <= 0);
                
                if (quantity == p.Quantity)
                {
                    Console.WriteLine( "You entered the same quantity, nothing have changed!" );
                }
                else
                {
                    p.Quantity = quantity;
                    Console.WriteLine("Quantity Updated!");
                }
                
                Console.WriteLine(p.ToString());
                break;
            }
        }
        
        Menu();
    }

    private static void DisplayDeleteProduct()
    {
        string name;
        Product? p = null;

        do
        {
            Console.WriteLine();
            Console.Write("Enter product name : ");
            name = Console.ReadLine() ?? string.Empty;

            if (name.Length <= 0)
            {
                Console.WriteLine("Empty name field !! TRY AGAIN");
                if (ExitCond() == -1)
                {
                    break;
                }
                continue;
            }

            p = Inventory.FindProduct(name);

            if (p != null) continue;
            
            name = "";
            Console.WriteLine("Product name doesn't exists !! TRY AGAIN");
            if (ExitCond() == -1)
            {
                break;
            }
        } while (name.Length <= 0);

        if( p != null)
            Inventory.DeleteProduct(p);
        
        Console.WriteLine();
        DisplayAllProducts();
    }

    //would like to implement this in the previous features but will keep it till the end to avoid conflicts
    private static int ExitCond()
    {
        Console.Write("To exit type e or E => ");
        var e = Console.ReadLine() ?? string.Empty;
        if (e.ToLower().Trim().Equals("e"))
        {
            return -1;
        }

        return 0;
    }

    private static void DisplaySearchProduct()
    {
        string name;
        Product? p = null;

        do
        {
            Console.WriteLine();
            Console.Write("Enter product name : ");
            name = Console.ReadLine() ?? string.Empty;

            if (name.Length <= 0)
            {
                Console.WriteLine("Empty name field !! TRY AGAIN");
                if (ExitCond() == -1)
                {
                    break;
                }
                continue;
            }

            p = Inventory.FindProduct(name);

            if (p != null) continue;
            
            name = "";
            Console.WriteLine("Product name doesn't exists !! TRY AGAIN");
            
            if (ExitCond() == -1)
            {
                break;
            }
        } while (name.Length <= 0);

        Console.WriteLine(p?.ToString());
        Menu();
    }
}