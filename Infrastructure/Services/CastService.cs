using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
	// to make this implementation is binding to a contract
	public class CastService : ICastService
	{
        // to call the repository methods (through Dependency Injection)
        private readonly ICastRepository _castRepository;

        // generate constructor
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }



        // to get cast detail
        public CastDetailsModel GetCastDetails(int id)
        {
            // to call Repository
            var castDetails = _castRepository.GetById(id);

            var cast = new CastDetailsModel
            {
                Id = castDetails.Id,
                Name = castDetails.Name,
                Gender = castDetails.Gender,
                TmdbUrl = castDetails.TmdbUrl,
                ProfilePath = castDetails.ProfilePath
            };

            // Movie:
            foreach (var movie in castDetails.MoviesOfCast)
            {
                cast.Movies.Add(new MovieModel { Id = movie.MovieId, Title = movie.Movie.Title, ReleaseDate = movie.Movie.ReleaseDate,
                    OriginalLanguage = movie.Movie.OriginalLanguage, PosterUrl = movie.Movie.PosterUrl });
            }


            return cast;
        }

        
    }
}

