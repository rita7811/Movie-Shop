using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
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
        public IEnumerable<Movie> Get30HighestGrossingMovies()
        {

            // LINQ code to get top 30 grossing movies
            // select top 30 * from Movie order by Revenue

            var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToList();
            return movies;
        }

        public IEnumerable<Movie> Get30HighestRatedMovies()
        {
            throw new NotImplementedException();
        }


        // method for movie detials page
        // override our basic methods
        public override Movie GetById(int id)
        {
            // include(join) lots of information
            // SELECT * FROM Movie JOIN Cast and MocieCast JOIN Trailer JOIN Genre and MovieGenre where id = id 
            var movieDetails = _dbContext.Movies
                .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
                .Include(m => m.Trailers)
                .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
                .FirstOrDefault(m => m.Id == id);
            return movieDetails;
        }

            


    }
}

