using System;
namespace ApplicationCore.Models
{
	public class UserDetailsModel
	{

        // constructor so that we don't have the null reference exception
        public UserDetailsModel()
        {
            Movies = new List<MovieModel>();
            Purchases = new List<PurchaseModel>();
            Reviews = new List<ReviewModel>();
            Favorites = new List<FavoriteModel>();
        }

        // properties
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }

        // list our properties in models
        public List<MovieModel> Movies { get; set; }
        public List<PurchaseModel> Purchases { get; set; }
        public List<ReviewModel> Reviews { get; set; }
        public List<FavoriteModel> Favorites { get; set; }
    }
}

