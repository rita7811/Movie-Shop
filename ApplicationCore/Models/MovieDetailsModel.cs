using System;
namespace ApplicationCore.Models
{
	public class MovieDetailsModel
	{
        // many many properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public int Runtime { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}

