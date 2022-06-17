using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
	// to make this implementation is binding to a contract
	public class MovieService : IMovieService
	{
		private readonly IMovieRepository _movieRepository;

		public MovieDetailsModel GetMovieDetails(int id)
        {
			var movie = new MovieDetailsModel
			{

			};

			return movie;
        }


        // Create method that return top movies to the caller
        // return list of movies
        public List<MovieCardModel> GetTopGrossingMovies()
        {

			var movies = _movieRepository.Get30HighestGrossingMovies();

			var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies)
            {
				movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

			return movieCards;



			// to call the movie repository to get the data from database
			//var movies = new List<MovieCardModel>
			//{
			//	new MovieCardModel { Id=1, PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg", Title="Inception"},
			//	new MovieCardModel { Id=2, PosterUrl="", Title=""},
			//	new MovieCardModel { Id=3, PosterUrl="", Title=""},
			//	new MovieCardModel { Id=4, PosterUrl="", Title=""},
			//	new MovieCardModel { Id=5, PosterUrl="", Title=""},
			//};

			//return movies;
        }

	}
}

