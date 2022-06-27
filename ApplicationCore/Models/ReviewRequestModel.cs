using System;
namespace ApplicationCore.Models
{
	public class ReviewRequestModel
	{
        public int UserId { get; set; }

        public int MovieId { get; set; }

        public int Rating { get; set; }

        public string ReviewText { get; set; }
    }
}

