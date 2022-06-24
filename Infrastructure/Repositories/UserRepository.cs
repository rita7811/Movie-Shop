using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
                                //each repository should be inherited from our repository basic class
                                                    //also should be inherited from our IUserrepository interface
    {
        // Generate constructor:

        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }




        // Implement interface:

        public Task<bool> CheckIfMoviePurchaseByUser(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            // if email exits, it gonna send me user object; if email doesn't exit, it will return me a null value
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}

