using System;
namespace ApplicationCore.Entities
{
	public class Purchase
	{
        // Properties
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid PurchaseNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int MovieId { get; set; }

        // Navigation Properties
        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}

