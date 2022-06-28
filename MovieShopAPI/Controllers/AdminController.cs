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
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;


        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> AddMovie([FromBody] MovieRequestModel model)
        {
            var movieAdd = await _adminService.AddMovie(model);
            if (movieAdd == true)
            {
                return Ok(movieAdd);
            }
            return NotFound(new { errorMessage = $"No Movie Added" });
        }



        [HttpGet]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieRequestModel model)
        {
            var movieUpdate = await _adminService.UpdateMovieDetails(model);
            if (movieUpdate == true)
            {
                return Ok(movieUpdate);
            }
            return NotFound(new { errorMessage = $"No Movie Update" });
        }


        [HttpGet]
        [Route("top-purchased-movies")]
        public async Task<IActionResult> TopPurchasedMovie(DateTime startdateTime, DateTime enddateTime)
        {
            var topPurchaseMovie = await _adminService.GetMoviePurchasedDetails(startdateTime);
            if(topPurchaseMovie != null)
            {
                return Ok(topPurchaseMovie);
            }
            return NotFound(new { errorMessage = $"No Movie Found" });
        }




    }
}
