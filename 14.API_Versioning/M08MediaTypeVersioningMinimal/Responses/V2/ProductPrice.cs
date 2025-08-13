namespace Responses.v2;

public class ProductPrice
{
    public decimal Ammount { get; set; }

    public string Currency { get; set; } = null!;

    public ProductPrice(decimal amount, string currency
    )
    {
        Ammount = amount;
        Currency = currency;
    }
    
    
}