using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("Api/Products")]
public class Product : Controller
{

    [HttpGet]
    public IActionResult getAllProducts()
    {
        return Ok(
           new
           {
               Message = "Articles from Controller",
               Listofarticles = new[] { "Product 01", "Product 02" }
           }
        );
    }

}
