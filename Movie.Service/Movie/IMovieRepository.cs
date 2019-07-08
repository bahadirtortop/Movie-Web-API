using Movie.Service.Movie.Model;
using Movie.Service.Repository;

namespace Movie.Service.Movie
{
    public interface IMovieRepository : IGenericRepository<Domain.Model.Movie>
    {
        MovieListDtoModel Get(string title);
        long Create(MovieEditDtoModel model);
        void Update(MovieEditDtoModel model);
    }
}
