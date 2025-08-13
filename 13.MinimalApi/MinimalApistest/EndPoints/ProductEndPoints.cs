
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Models;
using Data;
using Response;
using Requests;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;

namespace EndPoints;

public static class ProductEndPoints
{

    public static RouteGroupBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var Products = app.MapGroup("/api/products");

        Products.MapMethods("/", ["OPTIONS"], OptionsProducts);

        Products.MapMethods("/{productId:guid}", ["HEAD"], HeadProducts)
            .WithName("HeadProducts")
            .WithSummary("Check if a product exists by ID");

        Products.MapGet("/", GetPaged);
        
        Products.MapGet("/{productId:guid}", GetProductById)
                .WithName("GetProductById");

        Products.MapPost("/", CreateProduct);
        Products.MapPut("/{productId:guid}", UpdateProduct);
        Products.MapPatch("/{productId:guid}", PatchProduct);
        Products.MapDelete("/{productId:guid}", DeleteProduct);
        Products.MapPost("/process", ProcessAsync);
        Products.MapGet("/Status/{taskId:guid}", GetProcessStatus);
        Products.MapGet("/csvExporter", ExportProductsToCsv);
        Products.MapGet("PhysicalFile/", GetFiles);

        return Products;
    }


    private static IResult OptionsProducts(HttpResponse Response)
    {
        Response.Headers.Append("Allow", "GET, POST, PUT, DELETE, OPTIONS, HEAD");
        return Results.NoContent();
    }

    private static IResult HeadProducts(Guid productId, ProductRepository repo)
    {
        return repo.ExistsById(productId) ? Results.Ok() : Results.NotFound();
    }


    private static Results<Ok<ProductResponse>, NotFound> GetProductById(
                                                               ProductRepository repo,
                                                               Guid productId,
                                                               [FromQuery] bool includeReviews = false)
    {
        var product = repo.GetProductById(productId);

        if (product is null)
            return TypedResults.NotFound();

        var reviews = includeReviews ? repo.GetProductReviews(productId) : null;

        return TypedResults.Ok(ProductResponse.FromModel(product, reviews));
    }


    private static IResult GetPaged(
       ProductRepository repo,
       int page = 1,
       int pageSize = 10)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var products = repo.GetProductsPage(page, pageSize);

        var totalCount = repo.GetProductsCount();

        if (products.Count == 0)
            return Results.NotFound();

        var response = PagedResult<ProductResponse>.Create(
            ProductResponse.FromModel(products),
            page,
            pageSize,
            totalCount
        );

        return Results.Ok(response);
    }


    private static IResult CreateProduct(
       ProductRepository repo,
       Product product)
    {
        if (product is null)
            return Results.BadRequest("Product cannot be null");

        if (string.IsNullOrWhiteSpace(product.Name))
            return Results.BadRequest("Product name is required");

        if (product.Price <= 0)
            return Results.BadRequest("Product price must be greater than zero");

        var newproduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            Price = product.Price
        };


        if (!repo.AddProduct(newproduct))
            return Results.Conflict($"Product with name '{newproduct.Name}' already exists");


        return Results.CreatedAtRoute(
            "GetProductById",
            new { productId = newproduct.Id },
            ProductResponse.FromModel(newproduct)
        );

    }

    private static IResult UpdateProduct(
       ProductRepository repo,
       Guid productId,
       UpdateProductRequest updatedProduct)
    {
        if (updatedProduct is null)
            return Results.BadRequest("Product cannot be null");

        if (string.IsNullOrWhiteSpace(updatedProduct.Name))
            return Results.BadRequest("Product name is required");

        if (updatedProduct.Price <= 0)
            return Results.BadRequest("Product price must be greater than zero");

        var existingProduct = repo.GetProductById(productId);

        if (existingProduct is null)
            return Results.NotFound();



        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;

        if (!repo.UpdateProduct(existingProduct))
            return Results.StatusCode(500);



        return Results.NoContent();
    }
    private static async Task<IResult> PatchProduct(
       ProductRepository repo,
       Guid productId,
       HttpRequest request)
    {

        var stream = new StreamReader(request.Body);

        var json = await stream.ReadToEndAsync();

        var patchDocument = JsonConvert.DeserializeObject<JsonPatchDocument<UpdateProductRequest>>(json);

        if (patchDocument is null)
            return Results.BadRequest("Patch document cannot be null");

        var existingProduct = repo.GetProductById(productId);

        if (existingProduct is null)
            return Results.NotFound();

        //patchDoc.ApplyTo(existingProduct);



        if (!repo.UpdateProduct(existingProduct))
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

        return Results.NoContent();
    }

    private static IResult DeleteProduct(
       ProductRepository repo,
       Guid productId)
    {
        if (!repo.ExistsById(productId))
            return Results.NotFound();

        if (!repo.DeleteProduct(productId))
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

        return Results.NoContent();
    }

    private static IResult ProcessAsync()
    {
        var taskID = Guid.NewGuid();
        return Results.Accepted($"api/process/stutus/{taskID}",
       new
       {
           Status = "Processing",
           EstimatedCompletionTime =
           DateTime.UtcNow.AddMinutes(5)
       }

        );
    }

    private static IResult GetProcessStatus(Guid taskId)
    {
        // Simulate checking the status of a long-running process
        var Compledted = true; // This would be replaced with actual logic to check the process status

        var status = new
        {
            TaskId = taskId,
            Status = (Compledted) ? "Completed" : "In Process...",
            EstimatedCompletionTime = DateTime.UtcNow.AddMinutes(3)
        };

        return Results.Ok(status);
    }

    private static IResult ExportProductsToCsv(ProductRepository repo)
    {
        var products = repo.GetProductsPage(1, 1000); // Get all products, adjust as needed 

        if (products.Count == 0)
            return Results.NotFound("No products available for export");

        var csvContent = new System.Text.StringBuilder();

        csvContent.AppendLine("Id,Name,Price");

        foreach (var product in products)
        {
            csvContent.AppendLine($"{product.Id},{product.Name},{product.Price}");
        }


        var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent.ToString());

        return Results.File(bytes, "text/csv", "products.csv");
    }

    private static IResult GetFiles()
    {
        var file = Path.Combine(Directory.GetCurrentDirectory(), "Files", "products.csv");

        if (!System.IO.File.Exists(file))
            return Results.NotFound("File not found");

        return TypedResults.PhysicalFile(file, "text/csv", "products.csv");
    }

}
