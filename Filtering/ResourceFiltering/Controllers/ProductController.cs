using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace ActionFilters.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : Controller
{
    [HttpGet]
    
    public IActionResult GetProducts()
    {
        Thread.Sleep(2000); // Simulate a delay
        // This is where you would normally fetch products from a database or service
        return Ok(new[] { "Product1", "Product2", "Product3" });
    }


}
