using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {

        // call corresponding Repositories through Dependency Injection
        private readonly IUserRepository _userRepository;

        private readonly IPurchaseRepository _purchaseRepository;

        private readonly IFavoriteRepository _favoriteRepository;

        private readonly IReviewRepository _reviewRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }



        public async Task<bool> PurchaseMovie(PurchaseRequestModel model, int userId)
        {
            // check if the user already purchase this movie in the database, check by userId, movieId
            // -> we need to talk to the "PurchaseRepository class"
            var purchased = await _purchaseRepository.CheckIfMoviePurchaseByUser(model.UserId);

            // if this purchase history existed:
            if (purchased != null)
            {
                throw new ConflictException("You have purchased this movie before, enjoy your movie!");
            }

            // if this purchase history does not exist, continue with purchase:
            // 1. generate a new order Number
            var purchaseNumber = Guid.NewGuid();
            // 2. create Purchase object and save using Entity Framework (EF)
            var newPurchase = new Purchase
            {
                UserId = model.UserId,
                MovieId = model.MovieId,
                PurchaseNumber = purchaseNumber,
                TotalPrice = model.TotalPrice,
                PurchaseDateTime = DateTime.Now
            };
            // 3. save the purchase to purchase table using UserRepository, so that we need to send these information above to UserRepository
            var savedPurchase = await _purchaseRepository.Add(newPurchase);
            if (savedPurchase.Id > 0)
            {
                return true;
            }
            return false;
        }


        public async Task<PurchaseModel> IsMoviePurchased(PurchaseRequestModel model, int userId)
        {
            // go to database and get the row by userId and movieId
            var purchased = await _purchaseRepository.CheckIfMoviePurchaseByUser(userId);

            // 1. if this purchase history does not exist:
            if (purchased == null)
            {
                throw new ConflictException("You haven't purchased this movie yet!");
            }

            // 2. if this purchase history exist:
            var purchaseNumber = GetPurchaseNumber(userId, model.MovieId);

            if (purchaseNumber == purchased.PurchaseNumber.ToString())
            {
                var purchaseModel = new PurchaseModel
                {
                    UserId = purchased.UserId,
                    MovieId = purchased.MovieId,
                    //PurchaseNumber = purchased.PurchaseNumber,
                    PurchaseDateTime = purchased.PurchaseDateTime,
                    TotalPrice = purchased.TotalPrice
                };
                return purchaseModel;
            }
            return null;
        }


        public async Task<PagedResultSetModel<MovieCardModel>> GetAllPurchasesForUser(int userId, int pageSize = 20, int pageNumber = 1)
        {
            // to call method in Purchase Repository
            var purchases = await _purchaseRepository.GetAllPurchasesForUser(userId, pageSize, pageNumber);

            // return Task<List<MovieCardModel>>
            var movieCards = new List<MovieCardModel>();

            foreach (var purchase in purchases.PagedData)
            {
                movieCards.Add(new MovieCardModel { Id = purchase.Movie.Id, PosterUrl = purchase.Movie.PosterUrl, Title = purchase.Movie.Title});
            }
            return new PagedResultSetModel<MovieCardModel>(pageNumber, purchases.TotalRecoeds, pageSize, movieCards);
        }


        public async Task<UserDetailsModel> GetPurchasesDetails(int id)
        {
            // to call Repository
            var purchaseDetails = await _userRepository.GetById(id);

            // return model
            var purchase = new UserDetailsModel
            {
                Id = purchaseDetails.Id,
                FirstName = purchaseDetails.FirstName,
                LastName = purchaseDetails.LastName
            };

            // Movie:
            foreach (var movie in purchaseDetails.PruchasesOfUser)
            {
                purchase.Movies.Add(new MovieModel { Id = movie.MovieId, Title = movie.Movie.Title, PosterUrl = movie.Movie.PosterUrl});
            }

            return purchase;
        }


        public async Task<PagedResultSetModel<MovieCardModel>> GetAllFavoritesForUser(int userId, int pageSize = 20, int pageNumber = 1)
        {
            // to call method in Favorite Repository
            var favorites = await _favoriteRepository.GetAllFavoritesForUser(userId, pageSize, pageNumber);

            // return Task<List<MovieCardModel>>
            var movieCards = new List<MovieCardModel>();

            foreach (var favorite in favorites.PagedData)
            {
                movieCards.Add(new MovieCardModel { Id = favorite.Movie.Id, PosterUrl = favorite.Movie.PosterUrl, Title = favorite.Movie.Title });
            }
            return new PagedResultSetModel<MovieCardModel>(pageNumber, favorites.TotalRecoeds, pageSize, movieCards);
        }


        public async Task<PagedResultSetModel<ReviewCardModel>> GetAllReviewsByUser(int userId, int pageSize = 20, int pageNumber = 1)
        {
            // to call method in Review Repository
            var reviews = await _reviewRepository.GetAllReviewsByUser(userId);

            // return Task<List<ReviewModel>>
            var reviewCards = new List<ReviewCardModel>();

            foreach (var review in reviews.PagedData)
            {
                reviewCards.Add(new ReviewCardModel { UserId = review.UserId, MovieId = review.MovieId, Rating = review.Rating, ReviewText = review.ReviewText, MovieTitle = review.Movie.Title, MoviePosterUrl = review.Movie.PosterUrl });
            }
            return new PagedResultSetModel<ReviewCardModel>(pageNumber, reviews.TotalRecoeds, pageSize, reviewCards);
        }



        



        private string GetPurchaseNumber(int userId, int movieId)
        {
            var purchaseNumber = Guid.NewGuid().ToString();
            return purchaseNumber;
        }

    }
}

