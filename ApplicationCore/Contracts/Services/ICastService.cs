using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface ICastService
	{
        // to get cast detail
        Task<CastDetailsModel> GetCastDetails(int id);
    }
}

