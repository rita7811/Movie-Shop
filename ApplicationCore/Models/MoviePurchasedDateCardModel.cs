using System;
namespace ApplicationCore.Models
{
	public class MoviePurchasedDateCardModel
	{
        public DateTime PurchaseDate { get; set; }

        public int MovieId { get; set; }
        public string Title { get; set; }

    }
}

