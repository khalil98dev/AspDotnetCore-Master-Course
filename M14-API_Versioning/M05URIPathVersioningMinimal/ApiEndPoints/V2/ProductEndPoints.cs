
using Asp.Versioning;
using Asp.Versioning.Builder;
using Date;
using Microsoft.AspNetCore.Http.HttpResults;
using Modele;
using Responses.v2;

namespace PrtoductsController.V2;


public static class ProductEndPoints
{
    static public RouteGroupBuilder MapProductEndpointsV2(this IEndpointRouteBuilder app,ApiVersionSet apiVersionSet)
    {

        var produtcApi = app.MapGroup("/api/v{apiVersion:apiVersion}/products")
            .WithApiVersionSet(apiVersionSet);



        produtcApi.MapGet("{productId:guid}", GetProduct)
            .WithName("GetProductByIdV2")
            .HasApiVersion(2.0);

        return produtcApi;
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