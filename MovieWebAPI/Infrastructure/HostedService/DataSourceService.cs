using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Movie.Service.Movie;
using Movie.Service.Movie.Model;
using MovieWebAPI.Infrastructure.AppSettings;
using MovieWebAPI.Infrastructure.Mapping;
using MovieWebAPI.Model.Movie;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MovieWebAPI.Infrastructure.HostedService
{
    public class DataSourceService : HostedService
    {
        HttpClient _httpClient;
        DataSource _dataSource;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public DataSourceService(IOptions<DataSource> dataSourceOptions,IServiceScopeFactory serviceScopeFactory)
        {
            _httpClient = new HttpClient();
            _dataSource = dataSourceOptions.Value as DataSource;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
                await Update(cToken);
            }
        }

        public async Task Update(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var movieRepository = scope.ServiceProvider.GetRequiredService<IMovieRepository>();
                var movies = movieRepository.GetAll();
                foreach (var movie in movies)
                {
                    var response = await _httpClient.GetAsync($"{_dataSource.APIUrl}?t={movie.Title}&apiKey={_dataSource.APIKey}", cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var fact = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<MovieEditModel>(fact);
                        movieRepository.Update(data.MapTo<MovieEditDtoModel>());
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(_dataSource.UpdateFromMinutes), cancellationToken);
            }
        }
    }
}
