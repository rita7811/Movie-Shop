using System;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
	public class AccountController : Controller
	{
        [HttpGet]
        public async Task<IActionResult> Login()
        {
			return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)  //create object in "UserLoginModel" for (string email, string password)
        {
            return View();
        }

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
            return View();
        }


    }
}


