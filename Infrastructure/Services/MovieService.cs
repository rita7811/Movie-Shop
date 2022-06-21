using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
	// to make this implementation is binding to a contract
	public class MovieService : IMovieService
	{
		// to call the repository methods (through Dependency Injection)
		private readonly IMovieRepository _movieRepository;

		// generate constructor
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }



        public async Task<MovieDetailsModel> GetMovieDetails(int id)
        {
            // to call Repository
            var movieDetails = await _movieRepository.GetById(id);
            //var movieDetails = _movieRepository.GetById(id);

			// return model
			var movie = new MovieDetailsModel
			{
				// properties
				Id = movieDetails.Id,
				Tagline = movieDetails.Tagline,
				Title = movieDetails.Title,
				Price =movieDetails.Price,
				Overview = movieDetails.Overview,
				PosterUrl = movieDetails.PosterUrl,
				BackdropUrl = movieDetails.BackdropUrl,
				ImdbUrl = movieDetails.ImdbUrl,
				RunTime = movieDetails.RunTime,
				TmdbUrl = movieDetails.TmdbUrl,
                Revenue = movieDetails.Revenue,
				Budget = movieDetails.Budget,
                ReleaseDate = movieDetails.ReleaseDate
            };

			// Genre:
            foreach (var genre in movieDetails.GenresOfMovie)
            {
				movie.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });
																		// coming from second include
				// then, go to MovieDetailModel to create a constructor in order to initialize this collection.
			}

            // Trailer:
            foreach (var trailer in movieDetails.Trailers)
            {
				movie.Trailers.Add(new TrailerModel { Id = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl });
            }

			// Cast:
			foreach (var cast in movieDetails.CastsOfMovie)
			{
				movie.Casts.Add(new CastModel { Id = cast.CastId, Name = cast.Cast.Name, Character = cast.Character, ProfilePath = cast.Cast.ProfilePath });
			}

            // Review:
            foreach (var review in movieDetails.ReviewsOfMovie)
            {
                movie.Reviews.Add(new ReviewModel { MovieId = review.MovieId, Rating = review.Rating });
            }

            return movie;
        }





        // Create method that return top movies to the caller
        // return list of movies (model)  b/c we create models based on the views
        public async Task<List<MovieCardModel>> GetTopGrossingMovies()
        {
			
			var movies = await _movieRepository.Get30HighestGrossingMovies();
            //var movies = _movieRepository.Get30HighestGrossingMovies();

            // convert movies to list of movie into List<MovieCardModel> since return type is different
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

