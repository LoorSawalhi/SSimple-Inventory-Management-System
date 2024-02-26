namespace SimpleInventoryManagementSystem.Domain.ProductManagement;

public class Product
{
    private string _name;
    private float _price;
    private int _quantity;

    public Product(string name)
    {
        Name = name;
    }

    public Product(string name, float price, int quantity)
    {
        _name = name;
        _price = price;
        _quantity = quantity;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public float Price
    {
        get => _price;
        set
        {
            if(value > 0)
                _price = value;
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if(value > 0)
                _quantity = value;
        }
    }
}