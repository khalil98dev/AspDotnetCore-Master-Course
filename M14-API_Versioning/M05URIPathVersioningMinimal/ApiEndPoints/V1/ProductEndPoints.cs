
using Asp.Versioning;
using Asp.Versioning.Builder;
using Date;
using Microsoft.AspNetCore.Http.HttpResults;
using Modele;
using Responses.v1;

namespace PrtoductsController.V1;


public static class ProductEndPoints
{
    static public RouteGroupBuilder MapProductEndpointsV1(this IEndpointRouteBuilder app,ApiVersionSet apiVersionSet)
    {
        //Default Api 

        var defaultApi = app.MapGroup("/api/products")
            .WithApiVersionSet(apiVersionSet)
            .HasApiVersion(1.0);


        var produtc = app.MapGroup("/api/v{apiVersion:apiVersion}/products")
            .WithApiVersionSet(apiVersionSet)
            .HasApiVersion(1.0);

        defaultApi.MapGet("{productId:guid}", GetProduct)
        .WithName("GetProductByIdDefault");

    
        produtc.MapGet("{productId:guid}", GetProduct)
            .WithName("GetProductByIdV1");


        return produtc;
    }



    public static Results<Ok<ProductResponse>, NotFound> GetProduct(
        Guid productId,
        HttpResponse Response,
        ProductRepository _repository)
    {
        Response.Headers["Deprecated"] = "true";
        Response.Headers["Sunset"] = DateTime.UtcNow.AddDays(30).ToString("R");
        Response.Headers["Link"] = "</api/v2/Products/" + productId + ">; rel=\"upgrade\"";


        var product = _repository.GetProduct(productId);

        if (product == null || product.ID == Guid.Empty)
        {
            return TypedResults.NotFound();
        }

        // Assuming you have a method to convert Product to ProductResponse
        var response = ProductResponse.FromProduct(product);
        return TypedResults.Ok(response);
    }


}