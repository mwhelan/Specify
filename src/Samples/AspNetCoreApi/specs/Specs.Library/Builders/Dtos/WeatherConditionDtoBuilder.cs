using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using Specs.Library.Data;
using Specs.Library.Extensions;
using TestStack.Dossier;
using TestStack.Dossier.Lists;

namespace Specs.Library.Builders.Dtos
{
    public class WeatherConditionDtoBuilder : TestDataBuilder<WeatherConditionDto, WeatherConditionDtoBuilder>
    {
        public WeatherConditionDtoBuilder()
        {
            Set(x => x.ActiveFlag, true);
            Set(x => x.Condition, Any.Company.Name);
        }
        
        public static WeatherConditionDtoBuilder WithPersistedType()
        {
            var weatherType = Builder<WeatherType>.CreateNew().Persist();
            var weatherTypeDto = weatherType.MapTo(new WeatherTypeDto());
            return new WeatherConditionDtoBuilder()
                .Set(x => x.WeatherType, weatherTypeDto);
        }

        public static ListBuilder<WeatherConditionDto, WeatherConditionDtoBuilder> CreateListWithPersistedType(int size)
        {
            var weatherType = Builder<WeatherType>.CreateNew().Persist();
            var weatherTypeDto = weatherType.MapTo(new WeatherTypeDto());
            return WeatherConditionDtoBuilder.CreateListOfSize(size)
                .All()
                .Set(x => x.Id, 0)
                .Set(x => x.WeatherType, weatherTypeDto)
                .ListBuilder;
        }

    }
}