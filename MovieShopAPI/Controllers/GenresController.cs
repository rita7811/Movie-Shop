using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genresService;

        public GenresController(IGenreService genresService)
        {
            _genresService = genresService;
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Genres()
        {
            var genres = await _genresService.GetAllGenres();

            if (genres == null || !genres.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Genres Found" });
            }
            // 200
            return Ok(genres);
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddGenre([FromBody] GenreModel genre)
        {
            var addedGenre = await _genresService.AddGenre(genre);

            if (addedGenre == false)
            {
                // 404
                return NotFound(new { errorMessage = "No Genres Found" });
            }
            // 200
            return Ok(addedGenre);
        }


        [HttpDelete]
        [Route("deltet/id:int")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var deletedGenre = await _genresService.DeleteGenre(id);
            return Ok(deletedGenre);
        }


    }
}
