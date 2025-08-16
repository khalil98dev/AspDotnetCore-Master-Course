using M03.SecureRESTAPIWithJWTAuthentication.Permissions;
using M03.SecureRESTAPIWithJWTAuthentication.Requests;
using M03.SecureRESTAPIWithJWTAuthentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace M03.SecureRESTAPIWithJWTAuthentication.Controllers;


[ApiController]
[Route("api/token")]
public class JwtTokenController(JwtServiceProvider tokekenServiceProvider) : ControllerBase
{
    [HttpPost("generate")]
    public IActionResult GenerateToken(GenerateTokenRequest request)
    {
        return Ok(tokekenServiceProvider.GenerateToken(request));
    }


    [HttpPost("refresh-Token")]
    public IActionResult RefreshToken(RefreshTokenRequest refreshToken)
    {

        //Get Refresh Token from Db for our exceptioon we make a fake retrival with anonymose object 

        var refreshTokenUser = new
        {
            UserID = "b7e15162-8e67-4e4b-9c1a-1c2b3d4e5f6a",
            RefreshToken = "b3e1c2d4-5f6a-7b8c-9d0e-1f2a3b4c5d6e",
            Expires = DateTime.UtcNow.AddHours(12)
        };


        if (refreshToken is null
            || refreshTokenUser.Expires < DateTime.UtcNow
            || refreshToken.refreshToken != refreshTokenUser.RefreshToken)
        {
            return Problem(
                title: "Bad Request",
                statusCode: StatusCodes.Status400BadRequest,
                detail: "The refresh - Token is invalid or  expired"
            ); 
        }

        var user = new
        {
            Email = "khalil98dev@gmail.com",
            FirstName = "khalil",
            LastName = "bachir",
            Permissions = new List<string>{ Permission.Project.Create, Permission.Project.Read },
            Roles = new List<string>{"Admin"
}        };



        var generateTokenRequest = new GenerateTokenRequest
        {
            Id = refreshTokenUser.UserID,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Permissions = user.Permissions,
            Roles =user.Roles
        };

        return Ok(tokekenServiceProvider.GenerateToken(generateTokenRequest));
    }
}

