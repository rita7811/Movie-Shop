using System;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
	public class MoviesController : Controller
	{

		// call MovieService
		private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }



        // method: showing details of the movie
        public async Task<IActionResult> Details(int id)   //input parameter as integer id
		{
            var movie = await _movieService.GetMovieDetails(id);
            return View(movie);
        }


        // method: showing details of the Genre
        public async Task<IActionResult> Genre(int id, int pageSize = 30, int pageNumber = 1)
        {
            // call movie Service and get the data
            var pagedMovies = await _movieService.GetMoviesByGenre(id, pageSize, pageNumber);
            // send pagedMovies info to PagedMovies object
            return View("PagedMovies", pagedMovies);
        }

	}
}

