using System;
using System.Collections.Generic;
using ApiTemplate.Api.Application.Common;
using ApiTemplate.Api.Application.Common.Mappings;
using AutoMapper;

namespace Specs.Unit.ApiTemplate.Application.Common.Mappings
{
    public class AutoMapperSpecs : ScenarioFor<IMapper>
    {
        List<Type> _allMappedDestinationTypes;

        public void Given_the_classes_that_need_to_be_mapped()
        {
            _allMappedDestinationTypes = AssemblyScanner.AllMappedDestinations();
        }

        public void When_AutoMapper_is_configured()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.Advanced.AllowAdditiveTypeMapCreation = true;
                cfg.AddProfile<MappingProfile>();
            });

            SUT = configurationProvider.CreateMapper();

        }

        public void Then_the_configuration_is_valid()
        {
            SUT.ConfigurationProvider.AssertConfigurationIsValid();
        }

        //public void AndThen_AutoMapper_knows_how_to_map_the_source_to_destination()
        //{
        //    foreach (var destination in _allMappedDestinationTypes)
        //    {
        //        var source = destination.GetInterfaces()[0].GetGenericArguments()[0];

        //        var instance = TypeAccessor.Create(source).CreateNew();

        //        SUT.Map(instance, source, destination);
        //    }
        //}
    }
}