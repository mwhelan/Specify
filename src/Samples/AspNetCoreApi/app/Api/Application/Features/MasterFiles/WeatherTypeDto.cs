using ApiTemplate.Api.Application.Common.Mappings;
using ApiTemplate.Api.Application.Common.Validation;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentValidation;

namespace ApiTemplate.Api.Application.Features.MasterFiles
{
    public class WeatherTypeDto : MasterFileDto, IMapToAndFrom<WeatherType>, IRequireValidation
    {
        public virtual string WeatherTypeName { get; set; }
        public virtual bool RequiredFlag { get; set; }
    }

    public class WeatherTypeDtoValidator : PrimaryKeyValidator<WeatherTypeDto>
    {
        public WeatherTypeDtoValidator()
        {
            RuleFor(p => p.WeatherTypeName)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
