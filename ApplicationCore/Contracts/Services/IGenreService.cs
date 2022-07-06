using System;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IGenreService
	{
		Task<IEnumerable<GenreModel>> GetAllGenres();

		Task<bool> AddGenre(GenreModel model);
        Task<Genre> DeleteGenre(int id);

    }
}

