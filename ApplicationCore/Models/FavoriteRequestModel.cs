using System;
namespace ApplicationCore.Models
{
	public class FavoriteRequestModel
	{
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }
    }
}

