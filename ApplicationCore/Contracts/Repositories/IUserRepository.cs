using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IUserRepository : IRepository<User>
									//each repository gonna be inherited from our basic repository interface
												// User Entity
	{

		Task<User> GetUserByEmail(string email);


        //Task<Purchase> CheckIfMoviePurchaseByUser(int userId, int movieId);

        // for Purchases Page
        //Task<PagedResultSetModel<Purchase>> GetAllPurchasesForUser(int userId, int pageSize = 20, int pageNumber = 1);


        // for Favorites Page
        //Task<PagedResultSetModel<Favorite>> GetAllFavoritesForUser(int UserId, int pageSize = 20, int pageNumber = 1);


        // for Reviews Page
        //Task<PagedResultSetModel<Review>> GetAllReviewsByUser(int id, int pageSize = 20, int pageNumber = 1);
    }
}

