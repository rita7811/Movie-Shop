using System;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IAdminService
	{
        Task<bool> AddMovie(MovieRequestModel model);

        Task<bool> UpdateMovieDetails(MovieRequestModel model);

        Task<MoviePurchasedDetailsModel<MoviePurchasedDateCardModel>> GetMoviePurchasedDetails(DateTime dateTime);

    }
}

