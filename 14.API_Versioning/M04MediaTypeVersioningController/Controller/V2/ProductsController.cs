
using Date;
using Microsoft.AspNetCore.Mvc;
using Modele;
using Responses.v2;

namespace PrtoductsController.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/Products")]

public class ProductController(ProductRepository _repository) : ControllerBase
{
    [HttpGet("{productId:guid}")]
    public ActionResult<ProductResponse> GetProduct(Guid productId)
    {
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