using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {

        private readonly IRepository<Genre> _genreRepository;
        private readonly IGenreRepository _genreRepository1;

        public GenreService(IRepository<Genre> genreRepository, IGenreRepository genreRepository1)
        {
            _genreRepository = genreRepository;
            _genreRepository1 = genreRepository1;
        }



        public async Task<IEnumerable<GenreModel>> GetAllGenres()
        {
            var genres = await _genreRepository.GetAll();

            var genresModel = genres.Select(g => new GenreModel { Id = g.Id, Name = g.Name });
            return genresModel;
        }


        public async Task<bool> AddGenre(GenreModel model)
        {

            var genre = await _genreRepository1.GetById(model.Id);

            if (genre != null)   // user already exits 
            {
                // we can create a customer exception
                throw new ConflictException("Email already exists, please try to login.");
            }

            var newGenre = new Genre   // User Entity
            {
                Id = model.Id,
                Name = model.Name
            };

            var savedGenre = await _genreRepository1.Add(newGenre); // newly created user
            if (savedGenre.Id > 0)
            {
                return true;
            }
            return false;
        }


        public async Task<Genre> DeleteGenre(int id)
        {
            var getGenreToDelete = await _genreRepository1.GetById(id);
            var deletedGenre = await _genreRepository1.Delete(getGenreToDelete);
            return deletedGenre;
        }

    }
}

