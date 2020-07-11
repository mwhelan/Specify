using System.Collections.Generic;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using TestStack.Dossier;
using TestStack.Dossier.DataSources.Picking;
using TestStack.Dossier.Lists;

namespace Specs.Library.Builders.Entities.MasterFiles
{
    public class DisposalReasonBuilder : MasterFileBuilder<DisposalReason, DisposalReasonBuilder>
    {
        public DisposalReasonBuilder()
        {
            Set(x => x.Reason, Any.Company.Name);
            Set(x => x.DisposalReasonDescription, Any.Company.Location);
        }
    }

    public class DisposalReasonDtoBuilder : TestDataBuilder<DisposalReasonDto, DisposalReasonDtoBuilder>
    {
        public DisposalReasonDtoBuilder()
        {
        } 

        public virtual DisposalReasonDtoBuilder MapFromEntity(DisposalReason entity)
        {
            Set(x => x.Id, entity.Id);
            Set(x => x.Reason, entity.DisposalReasonDescription);
            Set(x => x.DisposalReasonDescription, entity.DisposalReasonDescription);
            Set(x => x.ActiveFlag, entity.ActiveFlag);

            return this;
        }

        public static ListBuilder<DisposalReasonDto, DisposalReasonDtoBuilder> CreateListFrom(
            List<DisposalReason> entities)
        {
            var dataSource = Pick.RepeatingSequenceFrom(entities);
            return DisposalReasonDtoBuilder.CreateListOfSize(entities.Count)
                .All()
                .With(builder =>
                {
                    var entity = dataSource.Next();
                    builder.MapFromEntity(entity);
                    return builder;
                });
        }

    }
}