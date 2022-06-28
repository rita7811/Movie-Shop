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
            var purchase = await _purchaseRepository.GetPurchaseByUser(userId, model.MovieId);

            // if this purchase history existed:
            if (purchase != null)
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
            var purchased = await _purchaseRepository.GetPurchaseByUser(userId, model.MovieId);

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
            var purchases = await _purchaseRepository.GetPurchaseByUser(userId, pageSize, pageNumber);

            // return Task<List<MovieCardModel>>
            var movieCards = new List<MovieCardModel>();

            foreach (var purchase in purchases.PagedData)
            {
                movieCards.Add(new MovieCardModel { Id = purchase.Movie.Id, PosterUrl = purchase.Movie.PosterUrl, Title = purchase.Movie.Title });
            }
            return new PagedResultSetModel<MovieCardModel>(pageNumber, purchases.TotalRecoeds, pageSize, movieCards);
        }


        public async Task<UserDetailsModel> GetPurchasesDetails(int userId, int movieId)
        {
            // to call Repository
            var purchaseDetails = await _userRepository.GetById(movieId);
            if (purchaseDetails == null)
            {
                return null;
            }
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
                purchase.Movies.Add(new MovieModel { Id = movie.MovieId, Title = movie.Movie.Title, PosterUrl = movie.Movie.PosterUrl });
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

        public async Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest)
        {

            var favorite = await _favoriteRepository.GetAllFavoritesForUser(favoriteRequest.UserId);

            if (favorite != null)
            {
                throw new ConflictException("You have added this movie as your favorite, enjoy your movie!");
            }
            else
            {
                var newFavorite = new Favorite
                {
                    Id = favoriteRequest.Id,
                    UserId = favoriteRequest.UserId,
                    MovieId = favoriteRequest.MovieId,
                };

                var savedFavorite = await _favoriteRepository.Add(newFavorite);
                if (savedFavorite.Id > 0)
                {
                    return true;
                }
                return false;
            }            
        }

        public async Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = await _favoriteRepository.GetAllFavoritesForUser(favoriteRequest.UserId);

            if (favorite == null)
            {
                throw new ConflictException("This movie is not in your favorite history.");
            }
            else
            {
                var deleteFavorite = new Favorite
                {
                    Id = favoriteRequest.Id,
                    UserId = favoriteRequest.UserId,
                    MovieId = favoriteRequest.MovieId,
                };

                var deletedFavorite = await _favoriteRepository.Delete(deleteFavorite);

                if (deletedFavorite.Id == null)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            // go to database and get the row by Id and movieId
            var favoritesForUser = await _favoriteRepository.GetAllFavoritesForUser(id);

            // 1. if this purchase history does not exist:
            if (favoritesForUser == null)
            {
                throw new ConflictException("You haven't add this movie to your favorite list!");
            }
            // 2. if this purchase history exist:
            else
            {
                return true;
            }
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


        public async Task<bool> AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = await _reviewRepository.GetAllReviewsByUser(reviewRequest.UserId);

            if (review != null)
            {
                throw new ConflictException("You have added review for this movie.");
            }
            else
            {
                var newReview = new Review
                {
                    UserId = reviewRequest.UserId,
                    MovieId = reviewRequest.MovieId,
                    Rating = reviewRequest.Rating,
                    ReviewText = reviewRequest.ReviewText
                };

                var savedReview = await _reviewRepository.Add(newReview);
                if (savedReview.UserId > 0 && savedReview.MovieId > 0)
                {
                    return true;
                }
                return false;
            }

        }

        public async Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = await _reviewRepository.GetAllReviewsByUser(reviewRequest.UserId);

            if (review != null) //update
            {
                var newReview = new Review
                {
                    UserId = reviewRequest.UserId,
                    MovieId = reviewRequest.MovieId,
                    Rating = reviewRequest.Rating,
                    ReviewText = reviewRequest.ReviewText
                };

                var updatedReview = await _reviewRepository.Update(newReview);
                
                return true;
            }
            else 
            {
                throw new ConflictException("You haven't have any review for this movie.");
            }

        }

        public async Task<bool> DeleteMovieReview(ReviewRequestModel reviewRequest, int userId, int movieId)
        {
            var review = await _reviewRepository.GetAllReviewsByUser(userId);

            if (review == null) 
            {
                throw new ConflictException("You don't have any review for this movie.");
            }
            else
            {
                var deleteReview = new Review
                {
                    UserId = reviewRequest.UserId,
                    MovieId = reviewRequest.MovieId,
                    Rating = reviewRequest.Rating,
                    ReviewText = reviewRequest.ReviewText
                };

                var deletedReview = await _reviewRepository.Delete(deleteReview);

                if (deletedReview == null)
                {
                    return true;
                }
                return false;
            }
        }





        private string GetPurchaseNumber(int userId, int movieId)
        {
            var purchaseNumber = Guid.NewGuid().ToString();
            return purchaseNumber;
        }



        public async Task<UserDetailsModel> GetUserDetails(int id)
        {
            var userDetails = await _userRepository.GetById(id);

            if (userDetails == null)
            {
                return null;
            }

            var user = new UserDetailsModel
            {
                Id = userDetails.Id,
                Email = userDetails.Email,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                DateOfBirth = userDetails.DateOfBirth,
                PhoneNumber = userDetails.PhoneNumber
            };

            foreach (var favorite in userDetails.FavoritesOfUser)
            {
                user.Favorites.Add(new FavoriteModel { UserId = favorite.UserId, MovieId = favorite.MovieId });
            }

            foreach (var purchase in userDetails.PruchasesOfUser)
            {
                user.Purchases.Add(new PurchaseModel
                {
                    Id = purchase.Id,
                    MovieId = purchase.MovieId,
                    UserId = purchase.UserId,
                    TotalPrice = purchase.TotalPrice,
                    PurchaseDateTime = purchase.PurchaseDateTime,
                    PurchaseNumber = purchase.PurchaseNumber
                });
            }

            foreach (var review in userDetails.ReviewsOfUser)
            {
                user.Reviews.Add(new ReviewModel
                {
                    UserId = review.UserId,
                    MovieId = review.MovieId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }
            return user;
        }

    }

}

