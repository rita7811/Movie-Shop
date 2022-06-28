using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<Purchase> GetPurchaseByUser(int userId, int movieId);

        Task<MoviePurchasedDetailsModel<Purchase>> GetMoviesPurchasedByDate(DateTime dateTime);

        // for Purchases Page
        Task<PagedResultSetModel<Purchase>> GetPurchaseByUser(int userId, int pageSize = 20, int pageNumber = 1);

    }
}

