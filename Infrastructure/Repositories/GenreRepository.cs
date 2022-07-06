using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Genre> GetById(int id)
        {
            
            var genreDetails = await _dbContext.Genres
                //.Include(m => m.MoviesOfGenre).ThenInclude(m => m.Movie)            
                .FirstOrDefaultAsync(m => m.Id == id);
            return genreDetails;

        }

    }
}

