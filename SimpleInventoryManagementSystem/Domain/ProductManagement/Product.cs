namespace SimpleInventoryManagementSystem.Domain.ProductManagement;

public class Product(string name, float price, int quantity)
{
    private string _name = name ;
    private float _price = price;
    private int _quantity = quantity;


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