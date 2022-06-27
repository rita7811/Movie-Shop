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

        public async Task<PagedResultSetModel<Favorite>> GetAllFavoritesForUser(int userId, int pageSize = 20, int pageNumber = 1)
        {
            // 1.get total count favorites for the user
            var totalFavoritesForUser = await _dbContext.Favorites.Where(u => u.UserId == userId).CountAsync();

            // 2. get pagination only certain records
            var favorites = await _dbContext.Favorites
                .Where(u => u.UserId == userId)
                .Include(t => t.User)
                .Include(t => t.Movie)
                .OrderByDescending(m => m.MovieId)
                .Select(m => new Favorite { Id = m.Id, UserId = m.UserId, MovieId = m.MovieId })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToArrayAsync();

            // 3. take favorites and put it into PagedResultSetModel
            var pagedFavorites = new PagedResultSetModel<Favorite>(pageNumber, totalFavoritesForUser, pageSize, favorites);

            return pagedFavorites;
        }
    }
}

