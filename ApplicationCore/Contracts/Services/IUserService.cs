using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IUserService
	{
        // 12 methods

        // for User
        Task<UserDetailsModel> GetUserDetails(int id);

        // for Purchases Page
        Task<bool> PurchaseMovie(PurchaseRequestModel model, int userId);   //create a PurchaseRequestModel class
        Task<PurchaseModel> IsMoviePurchased(PurchaseRequestModel model, int userId);
        Task<PagedResultSetModel<MovieCardModel>> GetAllPurchasesForUser(int id, int pageSize = 20, int pageNumber = 1);
        Task<UserDetailsModel> GetPurchasesDetails(int userId, int movieId);   //create a PurchaseDetailsModel class

        // for Favorites Page
        Task<PagedResultSetModel<MovieCardModel>> GetAllFavoritesForUser(int id, int pageSize = 20, int pageNumber = 1);   //create a FavoriteDetailsModel class ???
        Task<bool> FavoriteExists(int id, int movieId);
        Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest);

        // for Reviews Page
        Task<PagedResultSetModel<ReviewCardModel>> GetAllReviewsByUser(int id, int pageSize = 20, int pageNumber = 1);
        Task<bool> AddMovieReview(ReviewRequestModel reviewRequest);
        Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task<bool> DeleteMovieReview(ReviewRequestModel reviewRequest, int userId, int movieId);
    }
}

