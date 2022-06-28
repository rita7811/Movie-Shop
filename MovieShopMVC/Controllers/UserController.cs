using System;
using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers
{
    [Authorize]  // to make each every method has Filters
    public class UserController : Controller
	{
        // all these action methods should only be execute when user is loged in (using cookie to know if user is loged in)

        private readonly ICurrentLogedInUser _currentLogedInUser;

        private readonly IUserService _userService;

        public UserController(ICurrentLogedInUser currentLogedInUser, IUserService userService)
        {
            _currentLogedInUser = currentLogedInUser;
            _userService = userService;
        }



        [HttpGet]
        // write a code that will check if user is authen (if it is authecticated -> go to cookie and get information)
        // Filters
        //[Authorize]
		public async Task<IActionResult> Purchases(int id, int pageSize = 20, int pageNumber = 1)
        {
            // When we get Cookie back and we can get Cookie here, what should we do here now?
            // go to database and get all the movies purchased by user, user id (so we need to get user id from Cookie)
            // 1. get Cookie from HttpContext
            //var cookie = this.HttpContext.Request.Cookies["MovieShopAuthCookie"];  // based on our debug path (this means our controller object)
            //key for the Cookie
            // 2. decrypted to get user information
            // => we can create a class that exposes HttpContext cookie decrypted info and claims

            // Get UserId to call User Service:
            var userId = _currentLogedInUser.UserId;
            // Use the UserId and send to User Service to get information for that User

            // call User Service and get the data
            var pagedPurchases = await _userService.GetAllPurchasesForUser(id, pageSize, pageNumber);

            // send pagedPurchases info to PagedPurchases object
            return View("PagedPurchases", pagedPurchases);
        }


        [HttpPost]
        public async Task<IActionResult> PurchaseMovie(PurchaseRequestModel model)
        {
            var userId = _currentLogedInUser.UserId;

            // call RegisterUser() method:
            var purchase = await _userService.PurchaseMovie(model, userId);

            // redirect to Purchases page
            return RedirectToAction("Purchases");
        }



        [HttpGet]
		public async Task<IActionResult> Favorites(int id, int pageSize = 20, int pageNumber = 1)
        {
            var userId = _currentLogedInUser.UserId;

            // call User Service and get the data
            var pagedFavorites = await _userService.GetAllFavoritesForUser(id, pageSize, pageNumber);

            // send pagedFavorites info to PagedFavorites object
            return View("PagedFavorites", pagedFavorites);
        }


        [HttpPost]
        public async Task<IActionResult> AddFavorite()
        {
            var userId = _currentLogedInUser.UserId;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFavorite()
        {
            var userId = _currentLogedInUser.UserId;

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Reviews(int id, int pageSize = 20, int pageNumber = 1)
        {
            var userId = _currentLogedInUser.UserId;

            // call User Service and get the data
            var pagedReviews = await _userService.GetAllReviewsByUser(id, pageSize, pageNumber);

            // send pagedReviews info to PagedReviews object
            return View("PagedReviews", pagedReviews);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateReview()
        {
            var userId = _currentLogedInUser.UserId;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteReview()
        {
            var userId = _currentLogedInUser.UserId;

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = _currentLogedInUser.UserId;

            return View();
        }

	}
}

