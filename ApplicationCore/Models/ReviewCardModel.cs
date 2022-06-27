﻿using System;
namespace ApplicationCore.Models
{
	public class ReviewCardModel
    {
        // properties
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; }
        public string? MovieTitle { get; set; }
        public string? MoviePosterUrl { get; set; }
    }
}

