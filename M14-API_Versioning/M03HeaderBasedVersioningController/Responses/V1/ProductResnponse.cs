namespace Responses.v1;

using Modele;
public class ProductResponse
{
    public Guid ID { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }

    public ProductResponse(Guid id, string? name, decimal price)
    {
        ID = id;
        Name = name;
        Price = price;
    }

    //Mapper 

    public static ProductResponse FromProduct(Product product)
    {
        #if DEBUG
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        #endif
        // Map the properties from Product to ProductResponse
        return new ProductResponse(product.ID, product.Name, product.Price);
    }
    
    public static IEnumerable<ProductResponse> FromProducts(IEnumerable<Product> products)
    {
      
        if (products == null)
        {
            throw new ArgumentNullException(nameof(products), "Products cannot be null");
        }
        return products.Select(FromProduct);
    }

}