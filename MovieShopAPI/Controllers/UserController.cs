using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Details(int id)
        {
            var userDetails = await _userService.GetUserDetails(id);
            if (userDetails == null)
            {
                return NotFound(new { errorMessage = $"No Found User{id}" });
            }
            return Ok(userDetails);
        }

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> PurchaseMovie([FromBody] PurchaseRequestModel model, int userId)
        {
            var purchase = await _userService.PurchaseMovie(model, userId);
            return Ok(purchase);
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequestModel model, int userId)
        {
            var favoriteAdd = await _userService.AddFavorite(model);
            return Ok(favoriteAdd);
        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> UnFavorite([FromBody] FavoriteRequestModel model, int userId)
        {
            var favoriteDelete = await _userService.RemoveFavorite(model);
            return Ok(favoriteDelete);
        }

        [HttpGet]
        [Route("check-movie-favorite/{movieId:int}")]
        public async Task<IActionResult> CheckMovieFavorite(int userId, int movieId)
        {
            var favoriteCheck = await _userService.FavoriteExists(userId, movieId);
            if (favoriteCheck == true)
            {
                return Ok(favoriteCheck);                
            }
            return NotFound(new { errorMessage = $"No Found Movie{movieId}" });
        }

        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequestModel model, int userId)
        {
            var reviewAdd = await _userService.AddMovieReview(model);
            return Ok(reviewAdd);
        }

        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> EditReview([FromBody] ReviewRequestModel model, int userId)
        {
            var reviewEdit = await _userService.UpdateMovieReview(model);
            return Ok(reviewEdit);
        }

        [HttpDelete]
        [Route("delete-review")]
        public async Task<IActionResult> DeleteReview([FromBody] ReviewRequestModel model, int userId, int movieId)
        {
            var reviewDelete = await _userService.DeleteMovieReview(model, userId, movieId);
            return Ok(reviewDelete);
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> Purchases(int userId)
        {
            var purchases = await _userService.GetAllPurchasesForUser(userId);
            if (purchases == null)
            {
                return NotFound(new { errorMessage = $"No Found Any Purchase Records" });
            }
            return Ok(purchases);
        }

        [HttpGet]
        [Route("purchase-details/{movieId:int}")]
        public async Task<IActionResult> PurchaseDetails(int userId, int movieId)
        {
            var purchasesDetails = await _userService.GetPurchasesDetails(userId, movieId);
            if (purchasesDetails == null)
            {
                return NotFound(new { errorMessage = $"No Found Details of Movie{movieId}" });
            }
            return Ok(purchasesDetails);
        }

        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> CheckMoviePurchased([FromBody] PurchaseRequestModel model, int userId, int movieId)
        {
            var moviePurchasedCheck = await _userService.IsMoviePurchased(model, userId);
            if (moviePurchasedCheck == null)
            {
                return NotFound(new { errorMessage = $"No Found Movie{movieId} Purchase" });
            }
            return Ok(moviePurchasedCheck);
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> Favorites(int userId)
        {
            var favorites = await _userService.GetAllFavoritesForUser(userId);
            if (favorites == null)
            {
                return NotFound(new { errorMessage = $"No Found Favorites" });
            }
            return Ok(favorites);
        }

        [HttpGet]
        [Route("movie-reviews")]
        public async Task<IActionResult> MovieReviews(int userId)
        {
            var movieReviews = await _userService.GetAllReviewsByUser(userId);
            if (movieReviews == null)
            {
                return NotFound(new { errorMessage = $"No Found Any Movie Reviews" });
            }
            return Ok(movieReviews);
        }


    }
}
