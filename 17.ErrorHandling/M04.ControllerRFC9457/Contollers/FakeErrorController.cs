using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace M01.Overview.Contollers;

[Route("api/controller-fake-errors")]
[ApiController]
public class FakeErrorController : ControllerBase
{
    [HttpGet("server-error")]
    public IActionResult ServerErrorExample()
    {
        System.IO.File.ReadAllText(@"C:\Settings\SomeSettings.json"); // not exist

        return Ok();
    }

    [HttpPost("bad-request")]
    public IActionResult BadRequestExample() => BadRequest(
        new ProblemDetails
        {
            Title = "Bad Request",
            Type = "http:excemple.com/prop/req",
            Detail = "This item not found"
        }



    );


    [HttpPost("bad-request2")]
    public IActionResult BadRequestExample2() => Problem(
        title: "Bad Request",
        type: "http:excemple.com/prop/req",
        detail: "This item not found"
    );


    [HttpPost("not-found")]
    public IActionResult NotFoundExample() => NotFound("Product not found.");

    [HttpPost("unauthorized")]
    public IActionResult UnauthorizedExample() => Unauthorized();

    [HttpPost("conflict")]
    public IActionResult ConflictExample() => Conflict("This Product already exists.");


    [HttpPost("business-rule-error")]
    public IActionResult BusinessRuleExample() => throw new ValidationException("A discontinued product cannot be put on promotion.");

    [HttpGet("user-not-found")]
    public IActionResult Test() => throw new Exception("User not Found");

}