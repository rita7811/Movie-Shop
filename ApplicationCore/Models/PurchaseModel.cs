using System;
namespace ApplicationCore.Models
{
	public class PurchaseModel
	{
        // properties
        //public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public Guid PurchaseNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDateTime { get; set; }

    }
}

