using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IMovieService
	{
		// all the business functionality methods pertaining to Movies

		// MovieModel GetMovieDetails(int movieId)  --return type will be model



		List<MovieCardModel> GetTopGrossingMovies();

		// get movie details method
		MovieDetailsModel GetMovieDetails(int id);


	}
}

