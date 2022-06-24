using System;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
	public class UserController : Controller
	{

        // all these action methods should only be execute when user is loged in (using cookie to know if user is loged in)

        [HttpGet]
		public async Task<IActionResult> Purchases()
        {
			return View();
        }

        [HttpGet]
		public async Task<IActionResult> Favorites()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie()
        {
            return View();
        }


	}
}

