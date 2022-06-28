using System;
namespace ApplicationCore.Models
{
    public class PurchaseDetailsModel
    {
        public PurchaseDetailsModel()
        {
            Movies = new List<MovieModel>();
            Users = new List<UserModel>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid PurchaseNumber { get; set; }

        // list our properties in models
        public List<MovieModel> Movies { get; set; }
        public List<UserModel> Users { get; set; }
    }
}

