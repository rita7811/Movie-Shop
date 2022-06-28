using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
	{
        // Generate constructor:

        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        // Implement interface:

        //public async Task<Purchase> CheckIfMoviePurchaseByUser(int userId, int movieId)
        //{
        //    // if userId and movieId exit, it gonna send me user object; if they don't exit, it will return me a null value
        //    var purchase = await _dbContext.Purchases.Where(u => u.UserId == userId && u.MovieId == movieId).FirstOrDefaultAsync();
        //    return purchase;
        //}



        public async Task<PagedResultSetModel<Purchase>> GetPurchaseByUser(int userId, int pageSize = 20, int pageNumber = 1)
        {
            // 1.get total count purchases for the user
            var totalPurchasesForUser = await _dbContext.Purchases.Where(p => p.UserId == userId).CountAsync();

            // 2. get pagination only certain records
            var purchases = await _dbContext.Purchases
                .Where(p => p.UserId == userId)
                .Include(t => t.User)
                .Include(t => t.Movie)
                .OrderByDescending(m => m.PurchaseDateTime)
                .Select(m => new Purchase { Id = m.Id, UserId = m.UserId, MovieId = m.MovieId, TotalPrice = m.TotalPrice, PurchaseDateTime = m.PurchaseDateTime, PurchaseNumber = m.PurchaseNumber})
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            // 3. take purchases and put it into PagedResultSetModel
            var pagedPurchases = new PagedResultSetModel<Purchase>(pageNumber, totalPurchasesForUser, pageSize, purchases);
            return pagedPurchases;
            
        }


        public async Task<Purchase> GetById(int id)
        {
            var purchaseDetails = await _dbContext.Purchases
                .Include(m => m.Movie)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            return purchaseDetails;
        }

        public async Task<Purchase> GetPurchaseByUser(int userId, int movieId)
        {
            var purchase = await _dbContext.Purchases.FirstOrDefaultAsync(u => u.UserId == userId && u.MovieId == movieId);
            return purchase;
        }



        public async Task<MoviePurchasedDetailsModel<Purchase>> GetMoviesPurchasedByDate(DateTime dateTime)
        {
            // 1.get total count purchases based on time
            var totalMoviesByDate = await _dbContext.Purchases.Where(m => m.PurchaseDateTime == dateTime).CountAsync();

            // 2. get pagination only certain records
            var purchasesByDate = await _dbContext.Purchases
                .Where(m => m.PurchaseDateTime == dateTime)
                .Include(t => t.Movie)
                .OrderByDescending(m => m.PurchaseDateTime)
                .Select(m => new Purchase { Id = m.Id, UserId = m.UserId, MovieId = m.MovieId, TotalPrice = m.TotalPrice, PurchaseDateTime = m.PurchaseDateTime, PurchaseNumber = m.PurchaseNumber })
                .ToListAsync();

            var purchasedDateDetails = new MoviePurchasedDetailsModel<Purchase>(dateTime, totalMoviesByDate, purchasesByDate);
            return purchasedDateDetails;

        }


    }

}
