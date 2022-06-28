using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        
        public async Task<PagedResultSetModel<Favorite>> GetAllFavoritesForUser(int id, int pageSize = 20, int pageNumber = 1)
        {
            
            var totalFavoritesForUser = await _dbContext.Favorites.Where(u => u.Id == id).CountAsync();

            var favorites = await _dbContext.Favorites
                .Where(u => u.Id == id)
                .Include(t => t.User)
                .Include(t => t.Movie)
                .OrderByDescending(m => m.MovieId)
                .Select(m => new Favorite { Id = m.Id, UserId = m.UserId, MovieId = m.MovieId })
                .ToArrayAsync();

            
            var pagedFavorites = new PagedResultSetModel<Favorite>(pageNumber, totalFavoritesForUser, pageSize, favorites);

            return pagedFavorites;
        }


        
    }
}

