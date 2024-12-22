using Business_Logic_Layer;
using Entities_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WatchMovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private  IFilmService _filmService;

        public FilmsController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilms()
        {
            var films = await _filmService.GetAllFilmsAsync();
            return Ok(films);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmById(int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }

        [HttpPost]
        [AcceptVerbs("POST")]
        [ActionName("AddFilm")]

        public bool AddFsilm([FromBody] Film film)
        {
            try
            {   Film s =new Film();
                s= film;

                _filmService.AddFilmAsync(s);
                return true;
            }
            catch
            {
                return false;
            }

            //return CreatedAtAction(nameof(GetFilmById), new { id = film.Id }, film);

        }

        [HttpPost("{id}/watched")]
        public async Task<IActionResult> MarkAsWatched(int id, [FromBody] int userId)
        {
            await _filmService.MarkAsWatchedAsync(userId, id);
            return NoContent();
        }
    }
}
