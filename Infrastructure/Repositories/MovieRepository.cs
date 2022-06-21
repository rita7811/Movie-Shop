﻿using System;
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

        //public async Task<IEnumerable<Movie>> GetAverage()
        //{
        //    var movieDetails = await _dbContext.Movies.Select( a => new movieDetails { IDbContextFa})
        //        .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
        //        .Include(m => m.Trailers)
        //        .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
        //        .Include(m => m.ReviewsOfMovie)
        //        .Where()
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    return movieDetails;

        //    var genres = await _genreRepository.GetAll();

        //    var genresModel = genres.Select(g => new GenreModel { Id = g.Id, Name = g.Name });
        //    return genresModel;

        //}


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
                .FirstOrDefaultAsync(m => m.Id == id);
            return movieDetails;



            //var movieDetails = _dbContext.Movies
            //    .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
            //    .Include(m => m.Trailers)
            //    .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
            //    .FirstOrDefault(m => m.Id == id);
            //return movieDetails;
        }

            


    }
}

