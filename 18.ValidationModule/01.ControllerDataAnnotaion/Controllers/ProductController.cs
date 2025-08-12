

using ControllerDataAnnotation.Requests;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Products")]
public class ProductController : ControllerBase
{
    [HttpPost("Create")]
    public IActionResult Create(CreateProductRequest request)
    {

        return Created("Product Created", request);
    }

}