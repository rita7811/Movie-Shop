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


        public async Task<bool> AddMovieReview(ReviewRequestModel reviewRequest)
        {
            _dbContext.Set<ReviewRequestModel>().Add(reviewRequest);
            await _dbContext.SaveChangesAsync();

            if (reviewRequest.UserId > 0 && reviewRequest.MovieId > 0)
            {
                return true;
            }
            return false;
        }


        public async Task<bool> DeleteMovieReview(int userId, int movieId)
        {
            var review = await _dbContext.Reviews.Where(u => u.UserId == userId && u.MovieId == movieId).FirstOrDefaultAsync();
           
            if (review != null)
            {
                _dbContext.Remove(review);
                await _dbContext.SaveChangesAsync();
            }
            return true;

        }


        public async Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            _dbContext.Entry(reviewRequest).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResultSetModel<Review>> GetReviewsOfMovie(int movieId, int pageSize = 20, int pageNumber = 1)
        {
            var totalReviewsOfMovie = await _dbContext.Reviews.Where(i => i.MovieId == movieId).CountAsync();

            var reviews = await _dbContext.Reviews
                .Include(t => t.Movie)
                .Where(i => i.MovieId == movieId)
                .OrderByDescending(m => m.Rating)
                .Select(m => new Review { MovieId = m.MovieId, Rating = m.Rating, ReviewText = m.ReviewText, Movie = m.Movie, User = m.User })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagedReviewsOfMovie = new PagedResultSetModel<Review>(pageNumber, totalReviewsOfMovie, pageSize, reviews);

            return pagedReviewsOfMovie;
        }
    }
}

