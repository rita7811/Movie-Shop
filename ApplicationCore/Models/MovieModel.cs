using System;
namespace ApplicationCore.Models
{
	public class MovieModel
	{
        // properties
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? PosterUrl { get; set; }
        public string? OriginalLanguage { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}

