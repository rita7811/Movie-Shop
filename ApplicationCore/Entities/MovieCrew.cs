using System;
namespace ApplicationCore.Entities
{
	public class MovieCrew
	{
        // properties
        public int MovieId { get; set; }
        public int CrewId { get; set; }
        public string Department { get; set; }
        public string Job { get; set; }

        // Navigation Properties
        public Movie Movie { get; set; }
        public Crew Crew { get; set; }
    }
}

