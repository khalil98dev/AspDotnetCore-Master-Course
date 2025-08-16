using System.Threading.Tasks;
using DataProtection.Data;
using DataProtection.Modules;
using DataProtection.Requestes;
using DataProtection.Responses;
using DataProtection.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace DataProtection.Controllers;

[ApiController]
[Route("api/bids")]
public class BidController(IBidService bidService,CancellationToken ct=default) : ControllerBase
{

    [HttpPost("bid")]
    public async Task<ActionResult<BidResponse>> CreateAsync(BidRequest request)
    {
        if (request is null)
            return Problem(
                title: "Bad Request",
                detail: "Invalid request try to match the request object",
                statusCode: StatusCodes.Status400BadRequest
            );

        var result = await bidService.AddNewBidAsync(request, ct)!;

        if (result is null)
            return Problem(
                title: "Server Internal Error",
                statusCode: StatusCodes.Status500InternalServerError,
                detail: "Can't add this bid"
            );

        return  new CreatedResult($"api/bids/bid/{result.ID}",result);
    }

    [HttpGet]

    public async Task<IActionResult> GatAllBids()
    {
        var ressult = await bidService.GetAllBidsAsync(ct); 
        return Ok(ressult);
    }

    [HttpGet("{ID:guid}")]
    public IActionResult GatBidByID(Guid ID)
    {
        return Ok(bidService.GetBdByIDAsync(ID,ct));
    }
}