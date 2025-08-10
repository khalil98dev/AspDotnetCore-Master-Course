using Models;

public class ProductReviewResponse
{
    public Guid ReviewId { get; set; }
    public Guid ProductID { get; set; }

    public string? Reviewer { get; set; }

    public int Stars { get; set; }

    private ProductReviewResponse() { }

    public static ProductReviewResponse FromModel(ProductReview review)
    {
        if (review is null)
            throw new ArgumentNullException(nameof(review), "Cannot create instance from nullable data");


        var response = new ProductReviewResponse
        {
            ReviewId = review.Id,
            ProductID = review.ProductId,
            Reviewer = review.Reviewer,
            Stars = review.Stars
        };

        return response;
    }
    public static IEnumerable<ProductReviewResponse> FromModel(IEnumerable<ProductReview> reviews)
    {
         if (reviews is null)
            throw new ArgumentNullException(nameof(reviews));

        return reviews.Select(FromModel);    
    }

}




