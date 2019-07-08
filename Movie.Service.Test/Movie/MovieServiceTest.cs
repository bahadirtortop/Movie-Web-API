using Microsoft.EntityFrameworkCore;
using Movie.Domain;
using Movie.Service.Movie;
using Movie.Service.Movie.Model;
using NUnit.Framework;
using System.Linq;

namespace Movie.Service.Test.Movie
{
    [TestFixture]
    public class MovieServiceTest
    {
        private DbContextOptions<MovieContext> _dbContextOptions = new DbContextOptionsBuilder<MovieContext>().UseInMemoryDatabase("movieDb").Options;

        [Test]
        public void MovieService_Instance()
        {
            var context = new MovieContext(_dbContextOptions);
            {
                var movieService = new MovieService(context);

                Assert.IsInstanceOf<MovieService>(movieService);
            }
        }

        [Test]
        public void Return_Movie_Is_Not_Null()
        {
            //Arrange
            var movieSearch = GetSearch();
            var addMovieData = ReturnUpdateMovieData();
            var context = new MovieContext(_dbContextOptions);
            context.Set<Domain.Model.Movie>().Add(addMovieData);
            context.SaveChanges();

            //Act
            var movieService = new MovieService(context);
            var returnMovie = movieService.Get(movieSearch);

            //Assert
            Assert.IsInstanceOf<MovieListDtoModel>(returnMovie);
            Assert.IsNotNull(returnMovie);
        }

        [Test]
        public void Return_Movie_Null()
        {
            //Arrange
            var movieSearch = GetSearch();
            var context = new MovieContext(_dbContextOptions);

            //Act
            var movieService = new MovieService(context);
            var returnMovie = movieService.Get(movieSearch);

            //Assert
            Assert.IsNull(returnMovie);
        }

        [Test]
        public void Create_Movie_And_Movies_Count_Should_One()
        {
            var movie = ReturnAddMovieData();
            var context = new MovieContext(_dbContextOptions);
            {
                var movieService = new MovieService(context);
                movieService.Create(movie);

                Assert.AreEqual(1, context.Movies.Count());
            }
        }

        [Test]
        public void Update_Movie_And_Movie_Year_Should_2007()
        {
            //Arrange
            var context = new MovieContext(_dbContextOptions);
            var addMovieData = ReturnUpdateMovieData();
            var updateMovieData = ReturnAddMovieData();
            context.Set<Domain.Model.Movie>().Add(addMovieData);
            context.SaveChanges();

            //Act
            var movieService = new MovieService(context);
            movieService.Update(updateMovieData);

            //Assert
            Assert.AreEqual(2007,context.Movies.FirstOrDefault().Year);
        }

        private Domain.Model.Movie ReturnUpdateMovieData()
        {
            return new Domain.Model.Movie { Title = "Into The Wild", Year = 2009, Released = "19 Oct 2007", Runtime = "148 min", Genre = "Adventure, Biography, Drama", Director = "Sean Penn", Writer = "Sean Penn (screenplay), Jon Krakauer (book)", Actors = "Emile Hirsch, Marcia Gay Harden, William Hurt, Jena Malone", Language = "English, Danish", Country = "USA", Poster = "https://m.media-amazon.com/images/M/MV5BMTAwNDEyODU1MjheQTJeQWpwZ15BbWU2MDc3NDQwNw@@._V1_SX300.jpg", ImdbRating = "8.1" };
        }

        private MovieEditDtoModel ReturnAddMovieData()
        {
            return new MovieEditDtoModel { Title = "Into The Wild", Year = 2007, Released = "19 Oct 2007", Runtime = "148 min", Genre = "Adventure, Biography, Drama", Director = "Sean Penn", Writer = "Sean Penn (screenplay), Jon Krakauer (book)", Actors = "Emile Hirsch, Marcia Gay Harden, William Hurt, Jena Malone", Language = "English, Danish", Country = "USA", Poster = "https://m.media-amazon.com/images/M/MV5BMTAwNDEyODU1MjheQTJeQWpwZ15BbWU2MDc3NDQwNw@@._V1_SX300.jpg", ImdbRating = "8.1" };
        }
        private MovieListDtoModel GetMovie()
        {
            var movie = new MovieListDtoModel
            {
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
            return movie;
        }

        private string GetSearch()
        {
            return  "Into The Wild";
        }
    }
}
