
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Models;
using EFCoreBasicsImplementation.Data;
using Response;
using Requests;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using EFCoreBasicsImplementation.interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;


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

        Products.MapPost("/{productId:guid}/reviews", CreateReview)
                .WithName("CreateProductReview");
               
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

    private static async Task<IResult> HeadProducts(
        Guid productId,
        IUnitOfWork uow,
        CancellationToken ct = default)
    {
        return await uow.Products.ExistsByIdAsync(productId, ct) ? Results.Ok() : Results.NotFound();
    }


    private static async Task<Results<Ok<ProductResponse>, NotFound>> GetProductById(
                                                                IUnitOfWork uow,
                                                               Guid productId,
                                                               [FromQuery] bool includeReviews = false,
                                                               CancellationToken ct = default
                                                               )
    {
        var product = await uow.Products.GetProductByIdAsync(productId, ct);

        if (product is null)
            return TypedResults.NotFound();

        var reviews = includeReviews ? await uow.Products.GetProductReviewsAsync(productId, ct) : null;

        return TypedResults.Ok(ProductResponse.FromModel(product, reviews));
    }


    private static async Task<IResult> GetPaged(
        IUnitOfWork uow,
       int page = 1,
       int pageSize = 10,
       CancellationToken ct = default)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var products = await uow.Products.GetProductsPageAsync(ct, page, pageSize);

        var totalCount = await uow.Products.GetProductsCountAsync(ct);

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


    private static async Task<IResult> CreateProduct(
        IUnitOfWork uow,
       Product product,
       CancellationToken ct = default
       )
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

        uow.Products.AddProduct(newproduct);

        if (await uow.SaveChangesAsync(ct) <= 0)
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

        return Results.CreatedAtRoute(
            "GetProductById",
            new { productId = newproduct.Id },
            ProductResponse.FromModel(newproduct)
        );

    }

    private static async Task<IResult> UpdateProduct(
        IUnitOfWork uow,
       Guid productId,
       UpdateProductRequest updatedProduct,
       CancellationToken ct = default)
    {
        if (updatedProduct is null)
            return Results.BadRequest("Product cannot be null");

        if (string.IsNullOrWhiteSpace(updatedProduct.Name))
            return Results.BadRequest("Product name is required");

        if (updatedProduct.Price <= 0)
            return Results.BadRequest("Product price must be greater than zero");

        var existingProduct = await uow.Products.GetProductByIdAsync(productId, ct);

        if (existingProduct is null)
            return Results.NotFound();



        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;

        await uow.Products.UpdateProductAsync(existingProduct, ct);

        if (await uow.SaveChangesAsync(ct) <= 0)
            return Results.StatusCode(StatusCodes.Status500InternalServerError);


        return Results.NoContent();
    }
    private static async Task<IResult> PatchProduct(
        IUnitOfWork uow,
       Guid productId,
       HttpRequest request,
       CancellationToken ct = default
       )
    {

        var stream = new StreamReader(request.Body);

        var json = await stream.ReadToEndAsync();

        var patchDocument = JsonConvert.DeserializeObject<JsonPatchDocument<UpdateProductRequest>>(json);

        if (patchDocument is null)
            return Results.BadRequest("Patch document cannot be null");

        var existingProduct = await uow.Products.GetProductByIdAsync(productId, ct);

        if (existingProduct is null)
            return Results.NotFound();

        //patchDoc.ApplyTo(existingProduct);



        await uow.Products.UpdateProductAsync(existingProduct, ct);

        if (await uow.SaveChangesAsync(ct) <= 0)
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteProduct(
        IUnitOfWork uow,
       Guid productId,
       CancellationToken ct = default
       )
    {
        if (!await uow.Products.ExistsByIdAsync(productId, ct))
            return Results.NotFound();

        await uow.Products.DeleteProductAsync(productId, ct);

        if (await uow.SaveChangesAsync(ct) <= 0)
            return Results.StatusCode(StatusCodes.Status500InternalServerError);


        return Results.NoContent();
    }

    private static IResult ProcessAsync(CancellationToken ct = default)
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

    private static IResult GetProcessStatus(Guid taskId, CancellationToken ct = default)
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

    private static async Task<IResult> ExportProductsToCsv(IUnitOfWork uow, CancellationToken ct = default)
    {
        var products = await uow.Products.GetProductsPageAsync(ct, 1, 1000); // Get all products, adjust as needed 

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

    private static IResult GetFiles(CancellationToken ct = default)
    {
        var file = Path.Combine(Directory.GetCurrentDirectory(), "Files", "products.csv");

        if (!System.IO.File.Exists(file))
            return Results.NotFound("File not found");

        return TypedResults.PhysicalFile(file, "text/csv", "products.csv");
    }

    private static async Task<IResult> CreateReview(
        Guid productId,
        CreateProductReviewRequest review,
        IUnitOfWork uow,
        CancellationToken ct = default)
    {
        if(!await uow.Products.ExistsByIdAsync(productId, ct))
            return Results.NotFound($"Product with {productId} not found");

        if (review is null)
            return Results.BadRequest("Review cannot be null");

        if (string.IsNullOrWhiteSpace(review.Reviewer))
            return Results.BadRequest("Reviewer name is required");

        var newReview = new ProductReview
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Reviewer = review.Reviewer,
            Stars = review.Stars
        };
        await uow.Products.AddProductReviewAsync(newReview, ct);

        if (await uow.SaveChangesAsync(ct) <= 0)
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

        return Results.Created(
            $"/api/products/{productId}/reviews/{newReview.Id}",
            ProductReviewResponse.FromModel(newReview)  
        );   

    }
       

}
