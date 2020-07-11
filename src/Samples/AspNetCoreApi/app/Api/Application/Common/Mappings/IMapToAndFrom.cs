using AutoMapper;

namespace ApiTemplate.Api.Application.Common.Mappings
{
    public interface IMapToAndFrom<T>
    {
        void Mapping(Profile profile) => profile
            .CreateMap( GetType(), typeof(T))
            .ReverseMap();
    }
}