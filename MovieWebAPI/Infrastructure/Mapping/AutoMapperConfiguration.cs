using AutoMapper;
using Movie.Service.Movie.Model;
using MovieWebAPI.Model.Movie;

namespace MovieWebAPI.Infrastructure.Mapping
{
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// Initialize mapper
        /// </summary> 
        public static void Init()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                #region Movie
                cfg.CreateMap<MovieSearchDtoModel, MovieSearchModel>().ReverseMap();
                cfg.CreateMap<MovieEditDtoModel, MovieEditModel>().ReverseMap();
                cfg.CreateMap<MovieListDtoModel, MovieListModel>().ReverseMap();
                #endregion
            });
            Mapper = MapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Mapper
        /// </summary>
        public static IMapper Mapper { get; private set; }
        /// <summary>
        /// Mapper configuration
        /// </summary>
        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}
