﻿using System;
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
	}
}

