
using Date;
using Microsoft.AspNetCore.Mvc;
using Modele;
using Responses.v1;

namespace PrtoductsController.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/Products")]

public class ProductController(ProductRepository _repository) : ControllerBase
{

    
    [HttpGet("{productId:guid}")]
    public ActionResult<ProductResponse> GetProduct(Guid productId)
    {
        Response.Headers["Deprecated"] = "true";
        Response.Headers["Sunset"] = DateTime.UtcNow.AddDays(30).ToString("R"); 
        Response.Headers["Link"] = "</api/v2/Products/" + productId + ">; rel=\"upgrade\"";
        

        var product = _repository.GetProduct(productId);

        if (product == null || product.ID == Guid.Empty)
        {
            return NotFound();
        }

        // Assuming you have a method to convert Product to ProductResponse
        var response = ProductResponse.FromProduct(product);
        return Ok(response);
    }


}