using System;
namespace ApplicationCore.Models
{
	public class MovieDetailsModel
	{
        // many many properties

        // constructor so that we don't have the null reference exception
        public MovieDetailsModel()
        {
            Genres = new List<GenreModel>();
            Casts = new List<CastModel>();
            Trailers = new List<TrailerModel>();
        }


        // properties
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? PosterUrl { get; set; }
        public string? BackdropUrl { get; set; }
        public decimal? Rating { get; set; }
        public string? Overview { get; set; }
        public string? Tagline { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Revenue { get; set; }
        public string? ImdbUrl { get; set; }
        public string? TmdbUrl { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RunTime { get; set; }
        public decimal? Price { get; set; }


        // models for list of Genre, Cast and Trailer -> create GenreModel, CastModel and TrailerModel classes in Models
        // list our properties in models
        public List<GenreModel> Genres { get; set; }
        public List<CastModel> Casts { get; set; }
        public List<TrailerModel> Trailers { get; set; }

      
    }
}

