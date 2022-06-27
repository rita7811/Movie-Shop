using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IReviewRepository : IRepository<Review>
	{
        Task<PagedResultSetModel<Review>> GetAllReviewsByUser(int userId, int pageSize = 20, int pageNumber = 1);
    }
}

