
namespace Models;

public class Product
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public decimal Price { get; set; }

    public decimal AverageRating { get; set; }      

     public ICollection<ProductReview> Reviews { get; set; } = [];
    
}
