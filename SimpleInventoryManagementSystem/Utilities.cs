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
            Console.WriteLine("Enter your preferred option:\n" +
                              "1. Add new product.\n" +
                              "2. View all products.\n" +
                              "3. Edit a product.\n" +
                              "4. Delete a product.\n" +
                              "5. Search for a product.\n" +
                              "6. Exit.\n");

            var readLine = Console.ReadLine();

            if (readLine != null && int.TryParse(readLine, out var option))
                switch (option)
                {
                    case 1:
                        DisplayAddNewProductMenu();
                        break;
                    case 2:

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