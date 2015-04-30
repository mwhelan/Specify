using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace ContosoUniversity.Infrastructure.Mapping
{
    public class Mapper : IMapper
    {
        readonly IMappingEngine _mapper;

        public Mapper(IMappingEngine mapper)
        {
            _mapper = mapper;
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public virtual IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return source.Select(item => _mapper.Map<TSource, TDestination>(item));
        }

    }
}