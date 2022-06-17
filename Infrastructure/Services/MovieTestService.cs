using System;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class MovieTestService : IMovieService
    {
        public MovieDetailsModel GetMovieDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<MovieCardModel> GetTopGrossingMovies()
        {
            // going to no sql database and getting the data
            var movies = new List<MovieCardModel>
            {
                new MovieCardModel { Id=11, PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg", Title="Inception"},
                new MovieCardModel { Id=12, PosterUrl="", Title=""},
                new MovieCardModel { Id=13, PosterUrl="", Title=""},
                new MovieCardModel { Id=14, PosterUrl="", Title=""},
                new MovieCardModel { Id=15, PosterUrl="", Title=""},
            };

            return movies;
        }
    }
}

