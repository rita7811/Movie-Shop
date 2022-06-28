using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IMovieService
	{
		// all the business functionality methods pertaining to Movies

		// MovieModel GetMovieDetails(int movieId)  --return type will be model


		Task<List<MovieCardModel>> GetTopGrossingMovies();

        Task<List<MovieCardModel>> GetTopRatedMovies();

		Task<PagedResultSetModel<ReviewModel>> GetReviewsOfMovie(int id, int pageSize = 30, int pageNumber = 1);

        // get movie details method
        Task<MovieDetailsModel> GetMovieDetails(int id);

		// get movies by Genres
		Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenre(int id, int pageSize = 30, int pageNumber = 1);


    }
}

