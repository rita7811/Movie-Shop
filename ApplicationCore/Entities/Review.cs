using System;
namespace ApplicationCore.Entities
{
	public class Review
	{
        // properties
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public decimal Rating { get; set; }
        public string? ReviewText { get; set; }

        // Navigation Properties
        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}

