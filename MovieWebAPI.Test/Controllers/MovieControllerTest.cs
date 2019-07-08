using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Movie.Service.Movie;
using Movie.Service.Movie.Model;
using MovieWebAPI.Controllers;
using MovieWebAPI.Infrastructure.AppSettings;
using MovieWebAPI.Infrastructure.Mapping;
using MovieWebAPI.Model.Movie;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MovieWebAPI.Test.Controllers
{
    [TestFixture]
    public class MovieControllerTest
    {
        private Mock<IMovieRepository> _movieRepository;
        private IMemoryCache _memoryCache;
        private IOptions<DataSource> dataSourceOptions;
        private IOptions<Caching> cachingOptions;
        DataSource _dataSource;
        Caching _caching;


        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            _movieRepository = new Mock<IMovieRepository>();
            _memoryCache = serviceProvider.GetService<IMemoryCache>();
            dataSourceOptions = Options.Create(new DataSource());
            cachingOptions = Options.Create(new Caching {GetFromMinutes=12 });
            _dataSource = dataSourceOptions.Value;
            _caching = cachingOptions.Value;
            AutoMapperConfiguration.Init();

           
        }

        [Test]
        public void MovieController_IsInstanceOf_Test()
        {
            //Arrange
            var controller = new MovieController(_memoryCache,_movieRepository.Object,dataSourceOptions,cachingOptions);

            //Act | Assert
            Assert.IsInstanceOf<MovieController>(controller);
        }

        [Test]
        public void MovieController_Get_Should_Return_200_And_Data_Not_Null_Test()
        {
            //Arrange
            var movieSearch = GetSearch();
            var movie = GetMovie();
            _movieRepository.Setup(p => p.Get(movieSearch)).Returns(movie);
            var controller = new MovieController(_memoryCache,_movieRepository.Object,dataSourceOptions,cachingOptions);

            //Act
            var result = controller.Get(movieSearch);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(result);
        }

        private string GetSearch()
        {
            return "Into The Wild";
        }
        private MovieListDtoModel GetMovie()
        {
            return new MovieListDtoModel()
            {
                Id=1,
                Title = "Into The Wild",
                Year = 2007,
                Released = "19 Oct 2007",
                Runtime = "148 min",
                Genre = "Adventure, Biography, Drama",
                Director = "Sean Penn",
                Writer = "Sean Penn (screenplay), Jon Krakauer (book)",
                Actors = "Emile Hirsch, Marcia Gay Harden, William Hurt, Jena Malone",
                Language = "English, Danish",
                Country = "USA",
                Poster = "https://m.media-amazon.com/images/M/MV5BMTAwNDEyODU1MjheQTJeQWpwZ15BbWU2MDc3NDQwNw@@._V1_SX300.jpg",
                ImdbRating = "8.1"
            };
        }

    }
}
