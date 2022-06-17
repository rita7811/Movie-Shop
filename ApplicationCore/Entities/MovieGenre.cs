using System;
namespace ApplicationCore.Entities
{
	public class MovieGenre
	{
        // properties
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        // Navigation Properties (like Join table)
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}

