using ApiTemplate.Api.Application.Common.Mappings;
using ApiTemplate.Api.Application.Common.Validation;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentValidation;
using Newtonsoft.Json;

namespace ApiTemplate.Api.Application.Features.MasterFiles
{
    public class WeatherConditionDto : MasterFileDto, IMapToAndFrom<WeatherCondition>, IRequireValidation
    {
        public string Condition { get; set; }

        [JsonProperty]
        public WeatherTypeDto WeatherType { get; set; }
    }

    public class WeatherConditionDtoValidator : PrimaryKeyValidator<WeatherConditionDto>
    {
        public WeatherConditionDtoValidator()
        {
            RuleFor(p => p.Condition)
                .NotEmpty()
                .MaximumLength(200);

       //     RuleFor(p => p.WeatherTypeId).NotEmpty();
        }
    }
}
