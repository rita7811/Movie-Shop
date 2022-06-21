using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CastRepository : Repository<Cast>, ICastRepository
	{
		// dbContext
		public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }



        // Override GetById(int id) method in CastRepository class that will return Movies belonging to the cast including cast details
        // method for Cast detials page
        // override our basic methods
        public override async Task<Cast> GetById(int id)
        {
            // include(join) lots of information
            // SELECT * FROM Cast JOIN MocieCast JOIN Movie where id = id 
            var CastDetail = await _dbContext.Casts
                .Include(m => m.MoviesOfCast).ThenInclude(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            return CastDetail;

        }
    }
}

