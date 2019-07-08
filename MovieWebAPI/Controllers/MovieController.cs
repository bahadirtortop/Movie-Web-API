using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Movie.Service.Movie;
using Movie.Service.Movie.Model;
using MovieWebAPI.Infrastructure.AppSettings;
using MovieWebAPI.Infrastructure.Caching;
using MovieWebAPI.Infrastructure.Mapping;
using MovieWebAPI.Model.API;
using MovieWebAPI.Model.Movie;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace MovieWebAPI.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IMovieRepository _movieRepository;
        private readonly HttpClient _httpClient;
        DataSource _dataSource;
        Caching _caching;

        public MovieController(IMemoryCache memoryCache, IMovieRepository movieRepository, IOptions<DataSource> dataSourceOptions, IOptions<Caching> cachingOptions)
        {
            _memoryCache = memoryCache;
            _movieRepository = movieRepository;
            _httpClient = new HttpClient();
            _dataSource = dataSourceOptions.Value;
            _caching = cachingOptions.Value;
        }

        /// <summary>
        /// Return a movie.
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        /// <response code="200">Return a movie</response>
        /// <response code="404">Movie not found</response>
        [ProducesResponseType(typeof(MovieListModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult Get([FromQuery]string Title)
        {
            var key = CacheKeys.MovieList;
            if (string.IsNullOrEmpty(Title))
            {
                key += $"?{Title}";
            }
            if (!_memoryCache.TryGetValue(key, out object value))
            {
                var movie = _movieRepository.Get(Title);
                if (movie == null)
                {
                    var response = _httpClient.GetAsync($"{_dataSource.APIUrl}?t={Title}&apiKey={_dataSource.APIKey}").Result;

                    if (!response.IsSuccessStatusCode)
                        return NotFound();

                    var read = response.Content.ReadAsStringAsync();
                    if (read.Result.Contains("Error"))
                    {
                        var data = JsonConvert.DeserializeObject<APIResult>(read.Result);
                        return NotFound(new { data.Error });
                    }
                    else
                    {
                        var data = JsonConvert.DeserializeObject<MovieEditModel>(read.Result);
                        var d = _movieRepository.Create(data.MapTo<MovieEditDtoModel>());
                        value = data.MapTo<MovieListModel>();
                    }
                }
                else
                {
                    value = movie.MapTo<MovieListModel>();
                }
                _memoryCache.Set(key, value, DateTime.Now.AddMinutes(_caching.GetFromMinutes));
            }
            return Ok(value);
        }


    }
}
