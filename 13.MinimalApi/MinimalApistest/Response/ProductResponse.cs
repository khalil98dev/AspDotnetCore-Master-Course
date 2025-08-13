
using System.Diagnostics;
using Models;

namespace Response;


public class ProductResponse
{
    public Guid Id { get; set; }

    public string? NAme { get; set; }

    public decimal Price { get; set; }

    public List<ProductReviewResponse>? Reviews { get; set; } = default;

    private ProductResponse() { }

    private ProductResponse(Guid Id, string? NAme, decimal Price, List<ProductReviewResponse>? Reviews)
    {
        this.Id = Id;
        this.NAme = NAme;
        this.Price = Price;
        this.Reviews = Reviews;
    }

    public static ProductResponse FromModel(Product? product, IEnumerable<ProductReview>? Reviews = null)
    {

        if (product is null)
            throw new ArgumentNullException(nameof(product), "Cannot create instance from nullable data");



        ProductResponse respose = new ProductResponse
        {
            Id = product.Id,
            NAme = product.Name,
            Price = product.Price
        };
        if (Reviews is not null)
            respose.Reviews = ProductReviewResponse.FromModel(Reviews).ToList();

        return respose;
    }


    public static IEnumerable<ProductResponse> FromModel(IEnumerable<Product>? products,IEnumerable<ProductReview>? Reviews = null)
    {
        if (products is null)
            throw new ArgumentNullException(nameof(products));


        return products.Select(p=>FromModel(p)); 
    }


}

