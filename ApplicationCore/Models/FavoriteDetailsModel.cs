using System;
namespace ApplicationCore.Models
{
	public class FavoriteDetailsModel
	{
        // constructor so that we don't have the null reference exception
        public FavoriteDetailsModel()
        {
            Movies = new List<MovieModel>();
        }

        // properties
        public int UserId { get; set; }

        public int MovieId { get; set; }


        // list our properties in models
        public List<MovieModel> Movies { get; set; }
    }
}

