using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Favorite")]
    public class Favorite
	{
        // properties
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }

        // Navigation Properties
        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}

