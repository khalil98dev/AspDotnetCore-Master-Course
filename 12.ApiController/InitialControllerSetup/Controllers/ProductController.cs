
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Models;
using Data;
using Response;
using Requests;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.JSInterop.Infrastructure;

[ApiController]
[Route("api/products")]
public class ProductController(ProductRepository repo) : ControllerBase
{
    [HttpOptions]
    public IActionResult OptionsProducts()
    {
        Response.Headers.Append("Allow", "GET, POST, PUT, DELETE, OPTIONS,HEAD");
        return Ok("khalil");
    }

    [HttpHead("{productId:guid}")]
    public IActionResult HeadProducts(Guid productId)
    {
        return repo.ExistsById(productId) ? Ok() : NotFound();
    }

    [HttpGet("{productId:guid}", Name = "GetProductById")]
    public ActionResult<ProductResponse> GetProductById(Guid productId,
    [FromQuery] bool includeReviews = false)
    {
        var product = repo.GetProductById(productId);

        if (product is null)
            return NotFound();

        var reviews = includeReviews ? repo.GetProductReviews(productId) : null;

        return Ok(ProductResponse.FromModel(product, reviews));
    }

    [HttpGet]
    public IActionResult GetPaged(int page = 1, int pageSize = 10)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var products = repo.GetProductsPage(page, pageSize);

        var totalCount = repo.GetProductsCount();

        if (products.Count == 0)
            return NotFound();

        var response = PagedResult<ProductResponse>.Create(
            ProductResponse.FromModel(products),
            page,
            pageSize,
            totalCount
        );

        return Ok(response);
    }

    [HttpPost]
    public IActionResult CreateProduct(Product product)
    {
        if (product is null)
            return BadRequest("Product cannot be null");

        if (string.IsNullOrWhiteSpace(product.Name))
            return BadRequest("Product name is required");

        if (product.Price <= 0)
            return BadRequest("Product price must be greater than zero");

        var newproduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            Price = product.Price
        };


        if (!repo.AddProduct(newproduct))
            return Conflict($"Product with name '{newproduct.Name}' already exists");


        return CreatedAtRoute(
            "GetProductById",
            new { productId = newproduct.Id },
            ProductResponse.FromModel(newproduct)
        );

    }
    [HttpPut("{productId:guid}")]
    public IActionResult UpdateProduct(Guid productId, UpdateProductRequest updatedProduct)
    {
        if (updatedProduct is null)
            return BadRequest("Product cannot be null");

        if (string.IsNullOrWhiteSpace(updatedProduct.Name))
            return BadRequest("Product name is required");

        if (updatedProduct.Price <= 0)
            return BadRequest("Product price must be greater than zero");

        var existingProduct = repo.GetProductById(productId);

        if (existingProduct is null)
            return NotFound();



        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;

        if (!repo.UpdateProduct(existingProduct))
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Server can't Update product with id '{productId}'");


        return NoContent();
    }


    [HttpPatch("{productId:guid}")]
    public IActionResult PatchProduct(Guid productId, JsonPatchDocument<Product>? patchDoc)
    {
        if (patchDoc is null)
            return BadRequest("Patch document cannot be null");

        var existingProduct = repo.GetProductById(productId);

        if (existingProduct is null)
            return NotFound();

        patchDoc.ApplyTo(existingProduct);



        if (!repo.UpdateProduct(existingProduct))
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Server can't Patch product with id '{productId}'");

        return NoContent();
    }

    [HttpDelete("{productId:guid}")]
    public IActionResult DeleteProduct(Guid productId)
    {
        if (!repo.ExistsById(productId))
            return NotFound();

        if (!repo.DeleteProduct(productId))
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Server can't delete product with id '{productId}'");

        return NoContent();
    }

    [HttpPost("process")]
    public IActionResult ProcessAsync()
    {
        var taskID = Guid.NewGuid();
        return Accepted($"api/process/stutus/{taskID}",
       new
       {
           Status = "Processing",
           EstimatedCompletionTime =
           DateTime.UtcNow.AddMinutes(5)
       }

        );
    }

    [HttpGet("process/status/{taskId:guid}")]
    public IActionResult GetProcessStatus(Guid taskId)
    {
        // Simulate checking the status of a long-running process
        var Compledted = true; // This would be replaced with actual logic to check the process status

        var status = new
        {
            TaskId = taskId,
            Status = (Compledted) ? "Completed" : "In Process...",
            EstimatedCompletionTime = DateTime.UtcNow.AddMinutes(3)
        };

        return Ok(status);
    }


    [HttpGet("csvExporter")]
    public IActionResult ExportProductsToCsv()
    {
        var products = repo.GetProductsPage(1, 1000); // Get all products, adjust as needed 

        if (products.Count == 0)
            return NotFound("No products available for export");

        var csvContent = new System.Text.StringBuilder();

        csvContent.AppendLine("Id,Name,Price");

        foreach (var product in products)
        {
            csvContent.AppendLine($"{product.Id},{product.Name},{product.Price}");
        }


        var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent.ToString());

        return File(bytes, "text/csv", "products.csv");
    }

    [HttpGet("files")]
    public IActionResult GetFiles()
    {
        var file = Path.Combine(Directory.GetCurrentDirectory(), "Files", "products.csv");

        if (!System.IO.File.Exists(file))
            return NotFound("File not found");

        return PhysicalFile(file, "text/csv", "products.csv");  
    }

} 
