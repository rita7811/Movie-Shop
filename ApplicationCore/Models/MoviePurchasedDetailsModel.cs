using System;
namespace ApplicationCore.Models
{
	public class MoviePurchasedDetailsModel<T> where T : class
    {

        public MoviePurchasedDetailsModel(DateTime purchaseDate, int totalRecords, IEnumerable<T> pagedData)
        {
            PurchaseDate = purchaseDate;
            TotalRecords = totalRecords;
            PagedData = pagedData;
        }

        public DateTime PurchaseDate { get; set; }

        public int TotalRecords { get; }

        public IEnumerable<T> PagedData { get; set; }


    }
}

