using SimpleInventoryManagementSystem.Domain;

namespace SimpleInventoryManagementSystem;

using Domain.InventoryManagement;
using Domain.ProductManagement;

public class Utilities
{
    private ProductManagement _productManagement;

    public Utilities(ProductManagement productManagement)
    {
        _productManagement = productManagement;
    }

    private static Inventory Inventory { get; set; } = new();

    internal void InitializeInventory()
    {
        List<Product> products =
        [
            new Product("Laptop", 1500.00f, 10),
            new Product("Smartphone", 800.00f, 20),
            new Product("Tablet", 500.00f, 15),
            new Product("Phone", 10.00f, 10),
            new Product("Bottle", 20.00f, 20),
            new Product("Screen", 30.00f, 15),
            new Product("Car", 1580.00f, 10),
            new Product("Door", 870.00f, 20),
            new Product("Glasses", 300.00f, 15)];

        foreach (var product in products)
        {
            _productManagement.AddNewProduct(product);
        }
    }

    public void Menu()
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
                        DisplaySearchProduct();
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

    private void DisplayAddNewProductMenu()
    {
        var name = InputHandeller.ReadString("Enter product name : ");
        var findProduct = _productManagement.FindProduct(name);

        if (findProduct != null)
        {
            Console.WriteLine("Product name already exists !!");
            if (InputHandeller.ExitCond() == -1)
                Menu();
            DisplayAddNewProductMenu();
        }
        else
        {
            var price = InputHandeller.ReadNumber("Enter new price : ", Number.Float);
            var quantity = (int)InputHandeller.ReadNumber("Enter new quantity : ", Number.Integer);
            var product = new Product(name, price, quantity);
            _productManagement.AddNewProduct(product);

            Console.WriteLine("Product Added.");
        }

        Menu();
    }

    private void DisplayAllProducts()
    {
        var i = 1;
        Console.WriteLine("Inventory Products : ");

        foreach (var product in Inventory.Products)
        {
            Console.WriteLine(i + ". " + product);
            i += 1;
        }

        Menu();
    }

    private void DisplayEditProductList()
    {
        var product = FindProduct();
        if (product != null)
            EditProductList(product);
    }

    private void EditProductList(Product? product)
    {
        while (true)
        {
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
                        if (InputHandeller.ExitCond() == -1)
                            Menu();
                        continue;
                }
            }
            else
            {
                Console.WriteLine("Invalid Option !!! Try again.");
                if (InputHandeller.ExitCond() == -1)
                    Menu();
                continue;
            }

            break;
        }
    }

    private Product? FindProduct()
    {
        string name;
        Product? product;

        do
        {
            name = InputHandeller.ReadString("Enter product name : ");
            product = _productManagement.FindProduct(name);

            if (product != null) continue;

            name = "";
            Console.WriteLine("Product name doesn't exists !! TRY AGAIN");
            if (InputHandeller.ExitCond() == -1)
                Menu();
        } while (name.Length <= 0);

        return product;
    }

    private void EditProduct(int i, Product product)
    {
        switch (i)
        {
            case 1:
            {
                var name = InputHandeller.ReadString("Enter product name : ");

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
                var price = InputHandeller.ReadNumber("Enter new price : ", Number.Float);

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
                var quantity = (int)InputHandeller.ReadNumber("Enter new quantity : ", Number.Integer);

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

    private void DisplayDeleteProduct()
    {
        var product = FindProduct();

        if (product != null)
            _productManagement.DeleteProduct(product);

        Console.WriteLine();
        DisplayAllProducts();
    }

    private void DisplaySearchProduct()
    {
        string name;
        Product? product;

        do
        {
            name = InputHandeller.ReadString("Enter product name : ");

            product = _productManagement.FindProduct(name);

            if (product != null) continue;

            name = "";
            Console.WriteLine("Product name doesn't exists !! TRY AGAIN");

            if (InputHandeller.ExitCond() == -1)
                break;
        } while (name.Length <= 0);

        Console.WriteLine(product?.ToString());
        Menu();
    }
}