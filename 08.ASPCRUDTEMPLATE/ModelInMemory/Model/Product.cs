namespace Modeles;

public class Product
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public Product(Guid ID, string Name, decimal Price)
    {
        this.ID = ID;
        this.Name = Name;
        this.Price = Price;
    }

    public Product()
    {
        
    }
 
}



