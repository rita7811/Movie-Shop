using System;
namespace ApplicationCore.Models
{
	public class MoviePurchasedDetailsModel<T> where T : class
    {

        public MoviePurchasedDetailsModel(DateTime purchaseDate, int totalRecords, IEnumerable<T> pagedData)
        {
            PurchaseDate = purchaseDate;
            TotalRecoeds = totalRecords;
            PagedData = pagedData;
        }

        public DateTime PurchaseDate { get; set; }

        public int TotalRecoeds { get; }

        public IEnumerable<T> PagedData { get; set; }


    }
}

