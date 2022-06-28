using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IReviewRepository : IRepository<Review>
	{
        Task<PagedResultSetModel<Review>> GetAllReviewsByUser(int userId, int pageSize = 20, int pageNumber = 1);


        Task<PagedResultSetModel<Review>> GetReviewsOfMovie(int movieId, int pageSize = 20, int pageNumber = 1);

        Task<bool> AddMovieReview(ReviewRequestModel reviewRequest);
        Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task<bool> DeleteMovieReview(int userId, int movieId);

    }
}

