namespace SimpleInventoryManagementSystem.Domain.ProductManagement;


public class Product
{
    private int _id;
    private string _name;
    private float _price;
    private int _quantity;

    public Product(string name, float price, int quantity)
    {
        _name = name;
        _price = price;
        _quantity = quantity;
    }

    public Product(int id,string name, float price, int quantity )
    {
        _name = name;
        _price = price;
        _quantity = quantity;
        _id = id;
    }

    public string Name
    {
        get => _name;
        set => _name = value.ToLower().Trim();
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public float Price
    {
        get => _price;
        set
        {
            if (value > 0)
                _price = value;
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value > 0)
                _quantity = value;
        }
    }

    public override string ToString()
    {
        return $"Name : {Name}, Price :  {Price}, Quantity : {Quantity}.";
    }
}

