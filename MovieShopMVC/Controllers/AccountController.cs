using System;
using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
	public class AccountController : Controller
	{
        // call AccountService: use our User dependency Injection
        private readonly IAccountService _accountService;

        // Generate constructor:
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }



        // Login methods:

        [HttpGet]
        public async Task<IActionResult> Login()
        {
			return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)  //create object in "UserLoginModel" for (string email, string password)
        {
            // call ValidateUser() method:
            var user = await _accountService.ValidateUser(model.Email, model.Password);

            // return HomePage:
            if (user != null)   // if user is Valid for his Password
            {
                
                // create Cookie:

                // 1. create Claim
                var claims = new List<Claim>  // Claim already in our ASP.NET
                {
                    // these information will be stored inside the cookie

                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
                    new Claim(ClaimTypes.Country, "USA"),

                    new Claim("Language", "English"), //(key, value)
                };

                // 2. create a cookie and cookie will have the above claims information along with expiration time
                // don't store above information in the cookie as plain text, instead encrypt the above information

                // 2.1 we need to tell ASP.NET application that we are using Cookie based Authentication
                // so that ASP.NET can generate Cookie based on the settings we provide in "Program.cs)

                // 2.2 create a cookie:
                // Identity object that will identify the user with claims
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // ***Principal object which is used by ASP.NET to recognize the USER
                // Create the Cookie with above information

                // *****HttpContext object => Encapsualtes your Http Request information
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));


                // redirect to home page
                return LocalRedirect("~/");
            }

            return View(model);
        }



        // Register methods:

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            // show the empty register page when we make a GET Request
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)  //create object in "UserRegisterModel" for (string email, string password......)
        {
            // *****Model Binding

            // call RegisterUser() method:
            var user = await _accountService.RegisterUser(model);

            // redirect to Login page
            return RedirectToAction("Login");
        }
    }
}


