using System.Collections.Generic;

namespace MovieWebAPI.Infrastructure.Mapping
{
    public static class MappingProfile
    {
        public static TModel MapTo<TModel>(this object entity) where TModel : class
        {
            return (TModel)AutoMapperConfiguration.Mapper.Map(entity, entity.GetType(), typeof(TModel));
        }
        public static List<TModel> MapTo<TModel>(this IEnumerable<object> entityList) where TModel : class
        {
            return (List<TModel>)AutoMapperConfiguration.Mapper.Map(entityList, entityList.GetType(), typeof(List<TModel>));
        }
    }
}
