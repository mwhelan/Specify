using ApiTemplate.Api.Application.Common.Mappings;
using ApiTemplate.Api.Application.Common.Validation;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentValidation;

namespace ApiTemplate.Api.Application.Features.MasterFiles
{
    public class DisposalReasonDto : MasterFileDto, IMapToAndFrom<DisposalReason>, IRequireValidation
    {
        public virtual string Reason { get; set; }
        public virtual string DisposalReasonDescription { get; set; }
    }

    public class DisposalReasonDtoValidator : PrimaryKeyValidator<DisposalReasonDto>
    {
        public DisposalReasonDtoValidator()
        {
            RuleFor(p => p.Reason)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.DisposalReasonDescription)
                .MaximumLength(500);
        }
    }
}