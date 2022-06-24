using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IUserRepository : IRepository<User>
									//each repository gonna be inherited from our basic repository interface
												// User Entity
	{
		
		Task<User> GetUserByEmail(string email);

		Task<bool> CheckIfMoviePurchaseByUser(int userId, int movieId);
	}
}

