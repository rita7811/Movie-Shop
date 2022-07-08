using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMovieRepository _movieRepository;

        private readonly IPurchaseRepository _purchaseRepository;

        public AdminService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
        {
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
        }



        public async Task<bool> AddMovie(MovieRequestModel model)
        {
            var movie = await _movieRepository.GetById(model.Id);

            if (movie != null && movie.Title == model.Title)
            {
                throw new ConflictException("You already have this movie in System!");
            }
         
            var newMovie = new Movie
            {
                Id = model.Id,
                Title = model.Title,
                Overview = model.Overview,
                Tagline = model.Tagline,
                Budget = model.Budget,
                Revenue = model.Revenue,
                ImdbUrl = model.ImdbUrl,
                TmdbUrl = model.TmdbUrl,
                PosterUrl = model.PosterUrl,
                BackdropUrl = model.BackdropUrl,
                OriginalLanguage = model.OriginalLanguage,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                Price = model.Price,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                UpdatedBy = model.UpdatedBy,
                CreatedBy = model.CreatedBy
            };
            var savedMovie = await _movieRepository.Add(newMovie);
            if (savedMovie.Id > 0)
            {
                return true;
            }
            return false;
            
        }


        public async Task<MoviePurchasedDetailsModel<MoviePurchasedDateCardModel>> GetMoviePurchasedDetails(DateTime dateTime)
        {
            var moviePurchasedDetails = await _purchaseRepository.GetMoviesPurchasedByDate(dateTime);

            if (moviePurchasedDetails == null)
            {
                return null;
            }
            
            var moviePurchasedDateCard = new List<MoviePurchasedDateCardModel>();

            foreach (var movie in moviePurchasedDetails.PagedData)
            {
                moviePurchasedDateCard.Add(new MoviePurchasedDateCardModel { MovieId = movie.MovieId , PurchaseDate = movie.PurchaseDateTime, Title = movie.Movie.Title });
            }
            return new MoviePurchasedDetailsModel<MoviePurchasedDateCardModel>(dateTime, moviePurchasedDetails.TotalRecords, moviePurchasedDateCard);
            
        }


        public async Task<bool> UpdateMovieDetails(MovieRequestModel model)
        {
            var movieDetailsUpdate = await _movieRepository.GetById(model.Id);

            if (movieDetailsUpdate != null) //update
            {
                var newMovie = new Movie
                {
                    Id = model.Id,
                    Title = model.Title,
                    Overview = model.Overview,
                    Tagline = model.Tagline,
                    Budget = model.Budget,
                    Revenue = model.Revenue,
                    ImdbUrl = model.ImdbUrl,
                    TmdbUrl = model.TmdbUrl,
                    PosterUrl = model.PosterUrl,
                    BackdropUrl = model.BackdropUrl,
                    OriginalLanguage = model.OriginalLanguage,
                    ReleaseDate = model.ReleaseDate,
                    RunTime = model.RunTime,
                    Price = model.Price,
                    CreatedDate = model.CreatedDate,
                    UpdatedDate = model.UpdatedDate,
                    UpdatedBy = model.UpdatedBy,
                    CreatedBy = model.CreatedBy
                };

                var updatedMovie = await _movieRepository.Update(newMovie);

                return true;
            }
            else
            {
                throw new ConflictException("This movie does not exist.");
            }
        }


    }
}

