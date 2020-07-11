using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Domain.Model.MasterFiles;

namespace ApiTemplate.Api.Controllers.MasterFiles
{
    public class DisposalReasonController : MasterFileControllerFor<DisposalReason, DisposalReasonDto> { }
    public class WeatherTypeController : MasterFileControllerFor<WeatherType, WeatherTypeDto> { }
}