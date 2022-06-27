using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    // technically, this one has 7 methods
    // basic 5 methods are already comming from Repository<Movie>, so we find methods already implemented and don't need to implement all those 5 methods
    // Also, if we don't like basic methods, we have option to override them b/c we make them virtual
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        // dbContext
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }


        // using these repository to talk to our DB -> use dbContext -> go to Repository to make it access dbContext -> get dbContext
        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {

            // LINQ code to get top 30 grossing movies
            // select top 30 * from Movie order by Revenue

            // I/O bound operations
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;

            //var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToList();
            //return movies;
        }

        public Task<IEnumerable<Movie>> Get30HighestRatedMovies()
        {
            throw new NotImplementedException();
        }


        // method for movie detials page
        // override our basic methods
        public async Task<Movie> GetById(int id)
        {
            // include(join) lots of information
            // SELECT * FROM Movie JOIN Cast and MocieCast JOIN Trailer JOIN Genre and MovieGenre where id = id

            // I/O bound operations
            var movieDetails = await _dbContext.Movies
                .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
                .Include(m => m.Trailers)
                .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
                .Include(m => m.ReviewsOfMovie)
                //.Include(m => m.MoviesOfPurchase)
                .FirstOrDefaultAsync(m => m.Id == id);
            return movieDetails;


            //var movieDetails = _dbContext.Movies
            //    .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
            //    .Include(m => m.Trailers)
            //    .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
            //    .FirstOrDefault(m => m.Id == id);
            //return movieDetails;
        }


        public async Task<PagedResultSetModel<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            // go to movieGenre table included movies and make sure we get pagination only certain records

            // 1. get total count movies for the genre
            var totalMoviesForGenre = await _dbContext.MovieGenres.Where(g => g.GenreId == genreId).CountAsync();

            // 2. get pagination only certain records
            var movies = await _dbContext.MovieGenres
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Movie)
                .OrderByDescending(m => m.Movie.Revenue)
                .Select(m => new Movie { Id = m.MovieId, PosterUrl = m.Movie.PosterUrl, Title = m.Movie.Title })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            // take movies and put it into PagedResultSetModel
            var pagedMovies = new PagedResultSetModel<Movie>(pageNumber, totalMoviesForGenre, pageSize, movies);
            return pagedMovies;
        }


    }
}

