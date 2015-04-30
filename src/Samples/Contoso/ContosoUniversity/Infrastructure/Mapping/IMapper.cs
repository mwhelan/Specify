using System.Collections.Generic;

namespace ContosoUniversity.Infrastructure.Mapping
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);
    }
}