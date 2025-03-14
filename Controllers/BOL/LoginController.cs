using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Farm.Repositories;
using Farm.Models;
using Farm.Models.Lookup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Farm.Controllers;
using System.Text;
using Farm.Models.Data;

namespace Farm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class LoginController : Controller
    {
        private readonly ILoginsRepository loginsRepository;
        private readonly IEmployeesRepository employeesRepository;
        private readonly IAuthenticateRepository authenticateRepository;
        private readonly IRole_PrivilegesRepository role_PrivilegesRepository;
        private readonly IUsers_TypesRepository users_TypesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<LoginsController> logger;
        private readonly IConfiguration config;
        public LoginController(ILoginsRepository loginsRepository, IEmployeesRepository employeesRepository, 
        IRole_PrivilegesRepository role_PrivilegesRepository,
        IAuthenticateRepository authenticateRepository,
        IUsers_TypesRepository users_TypesRepository,
        IMapper mapper, ILogger<LoginsController> logger, IConfiguration config)
        {
            this.loginsRepository = loginsRepository;
            this.employeesRepository = employeesRepository;
            this.role_PrivilegesRepository = role_PrivilegesRepository;
            this.authenticateRepository = authenticateRepository;
            this.users_TypesRepository = users_TypesRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            Users users = this.authenticateRepository.ValidateCredentials(userName, password).Result;
            Users_Types userType = users_TypesRepository.GetUsers_TypesById((int)users.User_Type_Id).Result;
            if (users != null)
            {
                SessionData.UserId = users.Id;
                var role_priv = authenticateRepository.GetRole_PrivilegesByRole(users.User_Type_Id);
                var token = GenerateJwtToken(new { Employee = users, Privileges = role_priv });
                // var lookupRecentUpdates = authenticateRepository.GetLookupRecentUpdates();
                var lookupRecentUpdates = "";
                return Ok(new {
                    UserId = users.Id,
                    Token = token, 
                    LookupRecentUpdates = lookupRecentUpdates,
                    FirstName = users.First_Name, 
                    LastName = users.Last_Name, 
                    Role = userType.Name,
                    RoleId = userType.Id,
                    Role_Priv = role_priv 
                    });
            }
            else
            {
                return Ok(new { Token = "" });
            }
        }

        private string GenerateJwtToken(object userInfo)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("UserInfo", Newtonsoft.Json.JsonConvert.SerializeObject(userInfo)),
        };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
                return principal;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.ToString());
            }
            return null;
        }

        // public string ValidateJwtToken(string? token)
        // {
        //     if (token == null)
        //         return null;

        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
        //     try
        //     {
        //         tokenHandler.ValidateToken(token, new TokenValidationParameters
        //         {
        //             ValidateIssuerSigningKey = true,
        //             IssuerSigningKey = new SymmetricSecurityKey(key),
        //             ValidateIssuer = false,
        //             ValidateAudience = false,
        //             // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        //             ClockSkew = TimeSpan.Zero
        //         }, out SecurityToken validatedToken);

        //         var jwtToken = (JwtSecurityToken)validatedToken;
        //         var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

        //         // return account id from JWT token if validation successful
        //         return accountId;
        //     }
        //     catch
        //     {
        //         // return null if validation fails
        //         return null;
        //     }
        // }

        [HttpGet("~/GetMenus")]
        public async Task<IActionResult> GetMenus()
        {
            ClaimsPrincipal claims = null;
            try
            {
                if (this.HttpContext.Request.Headers.ContainsKey("access-token"))
                {
                    claims = this.AuthenticateUser(this.HttpContext.Request.Headers["access-token"]);
                }
                return Ok(claims);
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, ex.ToString());
                throw;
            }
        }

    }
}
