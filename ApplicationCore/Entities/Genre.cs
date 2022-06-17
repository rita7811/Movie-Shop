using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Genre")]
	public class Genre
	{
        // properties(columns):

        public int Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }


        // Navigation Properties
        public ICollection<MovieGenre> MoviesOfGenre { get; set; }
    }
}

