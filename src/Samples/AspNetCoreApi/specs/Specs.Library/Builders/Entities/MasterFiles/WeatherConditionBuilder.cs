using ApiTemplate.Api.Domain.Model.MasterFiles;
using Specs.Library.Data;
using TestStack.Dossier;
using TestStack.Dossier.Lists;

namespace Specs.Library.Builders.Entities.MasterFiles
{
    public class WeatherConditionBuilder : MasterFileBuilder<WeatherCondition, WeatherConditionBuilder>
    {
        public WeatherConditionBuilder()
        {
            Set(x => x.Condition, Any.Company.Name);
        }

        public static WeatherConditionBuilder WithPersistedType()
        {
            var weatherType = Builder<WeatherType>.CreateNew().Persist();
            return new WeatherConditionBuilder()
                .Set(x => x.WeatherType, weatherType);
        }

        public static ListBuilder<WeatherCondition, WeatherConditionBuilder> CreateListWithPersistedType(int size)
        {
            var weatherType = Builder<WeatherType>.CreateNew().Persist();
            return WeatherConditionBuilder.CreateListOfSize(size)
                .All()
                .Set(x => x.WeatherType, weatherType)
                .ListBuilder;
        }
    }
}