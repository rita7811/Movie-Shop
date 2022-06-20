using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface ICastService
	{
        // to get cast detail
        CastDetailsModel GetCastDetails(int id);
    }
}

