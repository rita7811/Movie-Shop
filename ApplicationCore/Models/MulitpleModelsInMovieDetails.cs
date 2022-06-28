using System;
namespace ApplicationCore.Models
{
	public class MulitpleModelsInMovieDetails
	{
		public List<MovieDetailsModel> MovieDetails { get; set; }
        public List<PurchaseRequestModel> PurchaseRequests { get; set; }
        public List<ReviewRequestModel> ReviewRequests { get; set; }
    }
}

