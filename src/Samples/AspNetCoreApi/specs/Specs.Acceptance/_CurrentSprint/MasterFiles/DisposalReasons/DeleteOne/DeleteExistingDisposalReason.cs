using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Library.Builders.Entities.MasterFiles;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.DisposalReasons.DeleteOne
{
    public class DeleteExistingDisposalReason : ScenarioFor<AsyncApiDriver, DisposalReasonStory>
    {
        private ApiResponse _result;
        private DisposalReason _existingItem;
        private List<DisposalReasonDto> _updates;

        public void Given_an_existing_master_file()
        {
            _existingItem = new DisposalReasonBuilder().Persist();
        }

        public async Task When_I_attempt_to_delete_it()
        {
            _result = await SUT.DeleteAsync(ApiRoutes.Master.DeleteFor<DisposalReason>(_existingItem.Id));
        }

        public void Then_the_file_should_be_deleted()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            Db.Set<DisposalReason>().Should().HaveCount(0);
        }
    }
}