using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Movie.Service.Movie;
using MovieWebAPI.Infrastructure.AppSettings;
using MovieWebAPI.Model.Movie;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MovieWebAPI.Infrastructure.HostedService
{
    public class DataSourceService : HostedService
    {
        HttpClient _httpClient;
        DataSource _dataSource;
        public DataSourceService(IOptions<DataSource> dataSourceOptions,IServiceScopeFactory serviceScopeFactory, IMovieRepository movieRepository)
        {
            _httpClient = new HttpClient();
            _dataSource = dataSourceOptions.Value as DataSource;
        }

        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
                //await Update(cToken);
            }
        }

        public async Task<MovieEditModel> GetAsync(string title)
        {
            var response = await _httpClient.GetAsync($"{_dataSource.APIUrl}?t={title}&apiKey={_dataSource.APIKey}");
            if (response.IsSuccessStatusCode)
            {
                var fact = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<MovieEditModel>(fact);
                return data;
            }
            return new MovieEditModel();
        }

        //public async Task Update(CancellationToken cancellationToken)
        //{
        //    var movies = _movieRepository.GetAll();
        //    foreach (var movie in movies)
        //    {
        //        var response = await _httpClient.GetAsync($"{_dataSource.APIUrl}?t={movie.Title}&apiKey={_dataSource.APIKey}", cancellationToken);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var fact = await response.Content.ReadAsStringAsync();
        //            var data = JsonConvert.DeserializeObject<MovieEditModel>(fact);
        //            Console.WriteLine($"{DateTime.Now.ToString()}\n{fact}");
        //        }
        //    }


        //    await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        //}
    }
}
