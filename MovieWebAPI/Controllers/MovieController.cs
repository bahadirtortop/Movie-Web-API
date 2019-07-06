using Microsoft.AspNetCore.Mvc;
using Movie.Service.Movie;
using Movie.Service.Movie.Model;
using MovieWebAPI.Infrastructure.Mapping;
using MovieWebAPI.Model.Movie;

namespace MovieWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        // GET: api/Movie
        [HttpGet]
        public IActionResult Get(MovieSearchModel model)
        {
            var movie = _movieRepository.Get(model.MapTo<MovieSearchDtoModel>()).MapTo<MovieListModel>();
            return Ok(movie);
        }

    }
}
