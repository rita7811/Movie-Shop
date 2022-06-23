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

        [HttpGet]
        public async Task<IActionResult> Login(UserLoginModel model)  //create object "UserLoginModel"
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            // show the empty register page when we make a GET Request
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register(UserRegisterModel model)  //create object "UserRegisterModel"
        {
            // *****Model Binding
            return View();
        }


    }
}


