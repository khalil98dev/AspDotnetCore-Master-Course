

using ControllerDataAnnotation.Requests;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] CreateProductRequest request)
    {
        // if (!ModelState.IsValid)
        //     return ValidationProblem(ModelState);
        return Created("Product Created", request);
    }

}