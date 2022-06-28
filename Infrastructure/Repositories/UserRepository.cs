using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;
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

        public async Task<User> GetUserByEmail(string email)
        {
            // if email exits, it gonna send me user object; if email doesn't exit, it will return me a null value
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }


        public async Task<User> GetById(int id)
        {
            var userDetails = await _dbContext.Users
                .Include(t => t.PruchasesOfUser).ThenInclude(m => m.Movie)
                .Include(t => t.ReviewsOfUser).ThenInclude(m => m.Movie)
                .Include(t => t.FavoritesOfUser).ThenInclude(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            return userDetails;
        }


    }
}

