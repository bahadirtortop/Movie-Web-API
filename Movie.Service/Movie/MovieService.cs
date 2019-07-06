using Movie.Domain;
using Movie.Service.Movie.Model;
using Movie.Service.Repository;
using System.Linq;

namespace Movie.Service.Movie
{
    public class MovieService : GenericRepository<Domain.Model.Movie>, IMovieRepository
    {
        public MovieService(MovieContext entities) : base(entities)
        {

        }

        public long Create(MovieEditDtoModel model)
        {
            var data = new Domain.Model.Movie();
            data.Title = model.Title;
            data.Year = model.Year;
            data.Released = model.Released;
            data.Runtime = model.Runtime;
            data.Genre = model.Genre;
            data.Director = model.Director;
            data.Writer = model.Writer;
            data.Actors = model.Actors;
            data.Language = model.Language;
            data.Country = model.Country;
            data.Poster = model.Poster;
            data.ImdbRating = model.ImdbRating;

            Add(data);
            return data.Id;
        }

        public MovieListDtoModel Get(MovieSearchDtoModel model)
        {
            var data = _context.Movies
                .FirstOrDefault(p=>(!string.IsNullOrEmpty(model.Title)&&p.Title.Contains(model.Title))
                                    ||string.IsNullOrEmpty(model.Title));

            if (data != null)
            {
                var movie = new MovieListDtoModel();
                movie.Id = data.Id;
                movie.Title = data.Title;
                movie.Year = data.Year;
                movie.Released = data.Released;
                movie.Runtime = data.Runtime;
                movie.Genre = data.Genre;
                movie.Director = data.Director;
                movie.Writer = data.Writer;
                movie.Actors = data.Actors;
                movie.Language = data.Language;
                movie.Country = data.Country;
                movie.Poster = data.Poster;
                movie.ImdbRating = data.ImdbRating;
                return movie;
            }
            return null;
        }

        public void Update(MovieEditDtoModel model)
        {
            var data = _context.Movies.FirstOrDefault(p => p.Title==model.Title);
            if (data == null)
                return;

            data.Year = model.Year;
            data.Released = model.Released;
            data.Runtime = model.Runtime;
            data.Genre = model.Genre;
            data.Director = model.Director;
            data.Writer = model.Writer;
            data.Actors = model.Actors;
            data.Language = model.Language;
            data.Country = model.Country;
            data.Poster = model.Poster;
            data.ImdbRating = model.ImdbRating;

            Edit(data);
        }
    }
}
