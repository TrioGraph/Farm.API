using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Farm.Models;
using Farm.Repositories;
using Farm.Controllers;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoginsController> logger;

    public AuthenticationMiddleware(RequestDelegate next, ILogger<LoginsController> logger)
    {
        _next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Perform authentication logic here
        if (context.Request.Path.Value.Equals("/Login"))
        {

        }
        else
        {
            var accessToken = context.Request.Headers["Authorization"];
            // var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            if (accessToken != "")
            {
                string[] tokenArray = accessToken.ToString().Split("Bearer ");
                if (tokenArray.Length == 2)
                {
                    accessToken = accessToken.ToString().Split("Bearer ")[1];
                    AuthenticateUser(accessToken);
                    // If authentication succeeds, set the user principal
                    var user = new Employees() { User_Name = "Pavan" };// Retrieve user based on credentials
                    var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.User_Name),
            // Add additional claims as needed
        };

                    var identity = new ClaimsIdentity(claims, "CustomAuth");
                    var principal = new ClaimsPrincipal(identity);
                    context.User = principal;
                }
            }
        }
        await _next(context);
    }


    private ClaimsPrincipal AuthenticateUser(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("this is my custom Secret key for authentication")),
            ValidateIssuer = false, // You can set this to true if you want to validate the issuer
            ValidateAudience = false, // You can set this to true if you want to validate the audience
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // You can adjust the acceptable clock skew
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
             dynamic claimObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(principal.Claims.First().Value);
             var privilegesList = claimObject.Privileges;
            //  Session["UserId"] = claimObject["Employee"]["Id"] as string;
            return principal;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.ToString());
        }
        return null;
    }

}
