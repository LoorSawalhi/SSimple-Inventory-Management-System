using SimpleInventoryManagementSystem.Domain;

namespace SimpleInventoryManagementSystem;

using Domain.InventoryManagement;
using Domain.ProductManagement;

internal static class Utilities
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
            Console.Write("""
                            Enter your preferred option:
                            
                            1. Add new product.
                            2. View all products.
                            3. Edit a product.
                            4. Delete a product.
                            5. Search for a product.
                            6. Exit.
                            
                            Option =
                          """);

            var readLine = Console.ReadLine();

            if (readLine != null && int.TryParse(readLine, out var option))
                switch (option)
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
                        // search a product
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid Option !!! Try again.");
                        continue;
                }
            else
                Console.WriteLine("Invalid Option !!! Try again.");

            break;
        }
    }

    private static void DisplayAddNewProductMenu()
    {
        var name = ReadString("Enter product name:");

        var findProduct = Inventory.FindProduct(name);

        if (findProduct != null)
        {
            Console.WriteLine("Product name already exists !!");
            DisplayAddNewProductMenu();
        }
        else
        {
            var price = ReadNumber("Enter new price : ", Number.Float);
            var quantity = (int)ReadNumber("Enter new quantity : ", Number.Integer);
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
            Console.WriteLine(i + ". " + product);
            i += 1;
        }

        Menu();
    }

    private static void DisplayEditProductList()
    {
        while (true)
        {
            string name;
            Product? product;

            do
            {
                name = ReadString("Enter product name:");

                product = Inventory.FindProduct(name);

                if (product != null) continue;

                name = "";
                Console.WriteLine("Product name doesn't exists !! TRY AGAIN");
            } while (name.Length <= 0);

            Console.Write("""
                              Which field would you like to edit : 
                              1. Name
                              2. Price
                              3. Quantity
                              4.Exit
                              
                              Option = 
                              """);

            var option = Console.ReadLine();

            Console.WriteLine();

            if (option != null && int.TryParse(option, out var i))
            {
                switch (i)
                {
                    case < 4 and > 0:
                        if (product != null) EditProduct(i, product);
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

    private static void EditProduct(int i, Product product)
    {
        switch (i)
        {
            case 1:
            {
                var name = ReadString("Enter product name:");

                if (name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("You entered the same name, nothing have changed!");
                }
                else
                {
                    product.Name = name;
                    Console.WriteLine("Name Updated!");
                }

                Console.WriteLine(product.ToString());
                break;
            }
            case 2:
            {
                var price = ReadNumber("Enter new price : ", Number.Float);

                if (Math.Abs(price - product.Price) < 0.00001)
                {
                    Console.WriteLine("You entered the same price, nothing have changed!");
                }
                else
                {
                    product.Price = price;
                    Console.WriteLine("Price Updated!");
                }

                Console.WriteLine(product.ToString());
                break;
            }
            case 3:
            {
                var quantity = (int)ReadNumber("Enter new quantity : ", Number.Integer);

                if (quantity == product.Quantity)
                {
                    Console.WriteLine("You entered the same quantity, nothing have changed!");
                }
                else
                {
                    product.Quantity = quantity;
                    Console.WriteLine("Quantity Updated!");
                }

                Console.WriteLine(product.ToString());
                break;
            }
        }

        Menu();
    }

    private static void DisplayDeleteProduct()
    {
        string name;
        Product? product;

        do
        {
            name = ReadString("Enter product name:");

            product = Inventory.FindProduct(name);

            if (product != null) continue;

            name = "";
            Console.WriteLine("Product name doesn't exists !! TRY AGAIN");
        } while (name.Length <= 0);

        if (product != null)
            Inventory.DeleteProduct(product);

        Console.WriteLine();
        DisplayAllProducts();
    }

    private static int ExitCond()
    {
        Console.Write("To exit type e or E => ");
        var e = Console.ReadLine() ?? string.Empty;
        if (e.ToLower().Trim().Equals("e")) return -1;

        return 0;
    }

    private static float ReadNumber(string message, Number numberType)
    {
        float number;
        do
        {
            var input = ReadLineInput(message);

            if (numberType == Number.Float)
            {
                if (float.TryParse(input, out number) && number > 0)
                    break;
            }
            else
            {
                if (int.TryParse(input, out var intNumber) && intNumber > 0)
                {
                    number = intNumber;
                    break;
                }
            }

            Console.WriteLine("Invalid input! It must be a number greater than 0. TRY AGAIN");
        } while (true);

        return number;
    }

    private static string ReadString(string message)
    {
        string input;
        do
        {
            input = ReadLineInput(message);
        } while (input.Length <= 0);

        return input;
    }

    private static string ReadLineInput(string message)
    {
        Console.Write(message);
        var input = Console.ReadLine() ?? string.Empty;

        Console.WriteLine();

        if (input.Length <= 0) Console.WriteLine("Empty field! TRY AGAIN");

        return input;
    }
}