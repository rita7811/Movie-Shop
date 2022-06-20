using System;
namespace ApplicationCore.Models
{
	public class CastDetailsModel
	{
		
		// many many properties

		// constructor so that we don't have the null reference exception
		public CastDetailsModel()
		{
			Movies = new List<MovieModel>();
		}


		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Gender { get; set; }
		public string? TmdbUrl { get; set; }
		public string? ProfilePath { get; set; }

		// list our properties in models
		public List<MovieModel> Movies { get; set; }
	
	}
}

