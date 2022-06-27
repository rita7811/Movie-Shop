using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IMovieRepository : IRepository<Movie>  //inherit from my base repository interface with movie entity(movie database table)
	{
		// all the database logic methods for Movies
		// Movie GetMovie(int id)  --return type will be Entities object (b/c delimited DB)

		// some common methods...like GetById(int id);

		Task<IEnumerable<Movie>> Get30HighestGrossingMovies();
		Task<IEnumerable<Movie>> Get30HighestRatedMovies();

		Task<PagedResultSetModel<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1);
	}
}

