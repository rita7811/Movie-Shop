using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var user = await _accountService.RegisterUser(model);
            return Ok(user);
        }


        [HttpGet]
        [Route("check-email")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var checkEmail = await _accountService.CheckEmail(email);
            if (checkEmail == true)
            {
                // 200
                return Ok(checkEmail);
            }
            // 404
            return NotFound(new { errorMessage = "No Found User Email" });
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model.Email, model.Password);

            if (user != null)
            {
                // create Jwt token
                var jetToken = CreateJwtToken(user);
                // once we have CreateJwtToken(user), we need to return this object
                return Ok(new { token = jetToken });
            }

            // Who calls this API with userName and password: IOS, Android, Angular, React, Java
            // We careated "token, **JWT (Json Web Token) https://jwt.io " instand of creating Cookie

            // Client will send email/password to API, POST
            // API will validate the email/password and if valid then API will create the JWT token and send to client
            // Its Client's reponsibility to store the token some where
            // Angular, React (store in localstorage or sessionstorage in brower)
            // IOS/Android, device (store in some where in device)

            // Once we have token:
            // When Client needs some secure information or needs to perform any operation that requires users to be authenticated
            // **Then client needs to send the token to the API in the Http Header

            // Once the API recieves the token from client,
            // it will validate the JWT token and if vlaid it will send the data back to the client
            // IF JWT token is invalid or token is expired, then API will send 401 Unauthorized

            throw new UnauthorizedAccessException("Please check email and password");
            //return Unauthorized(new { errorMessage = "Please check email and password" });
        }

        // Creating Jwt Token : we call this creation of token only when the user is validated
        private string CreateJwtToken(UserModel user)
        {
            // Create token (need packages: System.IdentityModel.Tokens.Jwt & Microsoft.IdentityModel.Tokens)

            // 1. create the claims and identity object:
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("Country", "USA"),
                new Claim("language", "english")
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // 2. specify a secret key:
            var secreKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));
                                                                        //read info in appsettings.json throigh inject Configuration

            // 3. specify the algorithm:
            var credentials = new SigningCredentials(secreKey, SecurityAlgorithms.HmacSha256);

            // 4. specify the expiration of the token:
            var tokenExpiration = DateTime.UtcNow.AddHours(2);

            // 5. create a object with all the above information to create the token:
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials = credentials,
                Issuer = "MovieShop, Inc",
                Audience = "MovieShop Clients"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.CreateToken(tokenDetails);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}
