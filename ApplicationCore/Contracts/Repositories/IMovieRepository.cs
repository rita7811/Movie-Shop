using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IMovieRepository : IRepository<Movie>  //inherit from my base repository interface with movie entity(movie database table)
	{
		// all the database logic methods for Movies
		// Movie GetMovie(int id)  --return type will be Entities object (b/c delimited DB)


		// some common methods...like GetById(int id);

		IEnumerable<Movie> Get30HighestGrossingMovies();
		IEnumerable<Movie> Get30HighestRatedMovies();

	}
}

