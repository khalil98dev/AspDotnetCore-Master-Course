namespace ControllerDataAnnotation.Requests;

public class CreateProductRequest
{

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? SKU { get; set; }

    public decimal? Price { get; set; }

    public int StockQuantity { get; set; }

    public DateTime LaunchDate { get; set; }

    public eProductCategory ProductCategory { get; set; }

    public string? ImageUrl { get; set; }

    public decimal? Wieght { get; set; }

    public int WarrantyPersiodMonths { get; set; }

    public bool IsReturnable { get; set; }

    public string? ReturnPolicyDescription { get; set; }
     
    public List<string> Tags { get; set; } = new();

}