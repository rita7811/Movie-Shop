using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResultSetModel<Review>> GetAllReviewsByUser(int userId, int pageSize = 20, int pageNumber = 1)
        {
            // 1.get total count reviews for the user
            var totalReviewsForUser = await _dbContext.Reviews.Where(u => u.UserId == userId).CountAsync();

            // 2. get pagination only certain records
            var reviews = await _dbContext.Reviews
                .Where(u => u.UserId == userId)
                .Include(t => t.User)
                .Include(t => t.Movie)
                .OrderByDescending(m => m.Rating)
                .Select(m => new Review { UserId = m.UserId, MovieId = m.MovieId, Rating = m.Rating, ReviewText = m.ReviewText, Movie = m.Movie, User = m.User })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            // 3. take reviews and put it into PagedResultSetModel
            var pagedReviews = new PagedResultSetModel<Review>(pageNumber, totalReviewsForUser, pageSize, reviews);

            return pagedReviews;
        }
    }
}

