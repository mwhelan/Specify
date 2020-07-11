using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace ApiTemplate.Api.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        private Assembly _assembly;

        public MappingProfile()
        {
            _assembly = Assembly.GetExecutingAssembly();

            ApplyMapFromMappings(typeof(IMapFrom<>), "IMapFrom`1");

            var type = typeof(IMapToAndFrom<>);

            ApplyMapFromMappings(typeof(IMapToAndFrom<>), "IMapToAndFrom`1");
        }

        private void ApplyMapFromMappings(Type interfaceType, string interfaceName)
        {
            var types = _assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => 
                    i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping") 
                    ?? type.GetInterface(interfaceName).GetMethod("Mapping");
                
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}