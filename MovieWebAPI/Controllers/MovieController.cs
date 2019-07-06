using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Movie.Service.Movie;
using Movie.Service.Movie.Model;
using MovieWebAPI.Infrastructure.AppSettings;
using MovieWebAPI.Infrastructure.Mapping;
using MovieWebAPI.Model.API;
using MovieWebAPI.Model.Movie;
using Newtonsoft.Json;
using System.Net.Http;

namespace MovieWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly HttpClient _httpClient;
        DataSource _dataSource;

        public MovieController(IMovieRepository movieRepository, IOptions<DataSource> dataSourceOptions)
        {
            _httpClient = new HttpClient();
            _dataSource = dataSourceOptions.Value as DataSource;
            _movieRepository = movieRepository;
        }
        // GET: api/Movie
        [HttpGet]
        public IActionResult Get([FromQuery]MovieSearchModel model)
        {
            var movie = _movieRepository.Get(model.MapTo<MovieSearchDtoModel>());
            if (movie==null)
            {
                var response = _httpClient.GetAsync($"{_dataSource.APIUrl}?t={model.Title}&apiKey={_dataSource.APIKey}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var fact = response.Content.ReadAsStringAsync();
                    if (fact.Result.Contains("Error"))
                    {
                        var data = JsonConvert.DeserializeObject<APIResult>(fact.Result);
                        return NotFound(new { data.Error });
                    }
                    else
                    {
                        var data = JsonConvert.DeserializeObject<MovieEditModel>(fact.Result);
                        var d = _movieRepository.Create(data.MapTo<MovieEditDtoModel>());
                        return Ok(new { data });
                    }
                }
            }
            return Ok(movie.MapTo<MovieListModel>());
        }

    }
}
