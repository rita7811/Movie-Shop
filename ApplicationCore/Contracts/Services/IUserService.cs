using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IUserService
	{
        // 12 methods



        // for Purchases Page
        Task<bool> PurchaseMovie(PurchaseRequestModel model, int userId);   //create a PurchaseRequestModel class
        Task<PurchaseModel> IsMoviePurchased(PurchaseRequestModel model, int userId);
        Task<PagedResultSetModel<MovieCardModel>> GetAllPurchasesForUser(int id, int pageSize = 20, int pageNumber = 1);
        Task<UserDetailsModel> GetPurchasesDetails(int id);   //create a PurchaseDetailsModel class

        // for Favorites Page
        Task<PagedResultSetModel<MovieCardModel>> GetAllFavoritesForUser(int id, int pageSize = 20, int pageNumber = 1);   //create a FavoriteDetailsModel class ???

        // for Reviews Page
        Task<PagedResultSetModel<ReviewCardModel>> GetAllReviewsByUser(int id, int pageSize = 20, int pageNumber = 1);

    }
}

