using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<Purchase> CheckIfMoviePurchaseByUser(int userId);

        // for Purchases Page
        Task<PagedResultSetModel<Purchase>> GetAllPurchasesForUser(int userId, int pageSize = 20, int pageNumber = 1);

    }
}

