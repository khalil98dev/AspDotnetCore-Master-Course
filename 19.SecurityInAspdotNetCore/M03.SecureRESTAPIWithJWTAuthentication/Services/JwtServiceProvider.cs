
using M03.SecureRESTAPIWithJWTAuthentication.Responses;
using M03.SecureRESTAPIWithJWTAuthentication.Requests;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace M03.SecureRESTAPIWithJWTAuthentication.Services;


public class JwtServiceProvider(IConfiguration Configuration)
{

    public TokenResponse GenerateToken(GenerateTokenRequest request)
    {
        IConfigurationSection JwtSettings = Configuration.GetSection("JwtSettings");

        var Issuer = JwtSettings["Isseur"]!;
        var Key = JwtSettings["Secretkey"]!;
        var Audience = JwtSettings["Audience"]!;
        var Expires = DateTime.UtcNow.AddMinutes(int.Parse(JwtSettings["ExpiredTokenInMinutes"]!.ToString()));


        var calims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub,request.Id!),
            new (JwtRegisteredClaimNames.Email,request.Email!),
            new (JwtRegisteredClaimNames.FamilyName,request.LastName!),
            new (JwtRegisteredClaimNames.GivenName,request.FirstName!),
            new (JwtRegisteredClaimNames.Sub,request.Id!),

        };

        request.Roles.ForEach(r => calims.Add(new(ClaimTypes.Role, r)));

        request.Permissions.ForEach(p => calims.Add(new("Permission", p)));


        var Descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(calims),
            Issuer = Issuer,
            Expires = Expires,
            Audience = Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)),
                SecurityAlgorithms.HmacSha256
            )
        };

        var TokenHandler = new JwtSecurityTokenHandler();

        var Security = TokenHandler.CreateToken(Descriptor);

        return new TokenResponse
        {
            AccessToken = TokenHandler.WriteToken(Security),
            RefreshToken = "2168123134687614631",
            Expires = Expires
        };
        

    }   
}


