using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Movies(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenre(genreId);
            if (movies == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for {genreId}" });
            }
            return Ok(movies);
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for {id}" });
            }
            return Ok(movie);
        }


        [HttpGet]
        [Route("top-rated")]
        public async Task<IActionResult> TopRatedMovies()
        {
            var movies = await _movieService.GetTopRatedMovies();
            if (movies == null || !movies.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found" });
            }
            // 200
            return Ok(movies);
        }


        // for our home page :
        [HttpGet]
        [Route("top-grossing")]  // **Attribute Routing
                                 // MVC http://localhost/movies/GetTopGrossingMovies => Traditional/Convention based routing
                                 // http://localhost/api/movies/top-grossing
        public async Task<IActionResult> GetTopGrossingMovies()
        {
            // call our service
            var movies = await _movieService.GetTopGrossingMovies();

            // return the movies information in JSON Format
            // ASP.NET Core automatically serializes C# objects to JSON objectes
            // "System.Text.Json" .NET 3
            // older version of .NET to work with JSON Newtonsoft.JSON
            // **return data(json format) along with it return the HTTP status code

            if (movies == null || !movies.Any())  // return true or false
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found" });
            }
            // 200
            return Ok(movies);
        }


        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> Genres(int genreId)
        {
            var moviesForGenres = await _movieService.GetMoviesByGenre(genreId);
            if (moviesForGenres == null)
            {
                // 404
                return NotFound(new { errorMessage = $" No Movies of Genre{genreId} Found" });
            }
            // 200
            return Ok(moviesForGenres);
        }


        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReview(int id)
        {
            var movieReview = await _movieService.GetReviewsOfMovie(id);
            if(movieReview == null)
            {
                // 404
                return NotFound(new { errorMessage = $" No Reviews of Movie{id} Found" });
            }
            // 200
            return Ok(movieReview);
        }

    }
}
