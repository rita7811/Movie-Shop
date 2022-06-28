using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IFavoriteRepository : IRepository<Favorite>
	{
        Task<PagedResultSetModel<Favorite>> GetAllFavoritesForUser(int id, int pageSize = 20, int pageNumber = 1);

        

    }
}

