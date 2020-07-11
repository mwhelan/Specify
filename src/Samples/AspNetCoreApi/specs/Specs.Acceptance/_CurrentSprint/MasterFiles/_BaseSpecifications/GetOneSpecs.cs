using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Domain.Common;
using FluentAssertions;
using Specify.Stories;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;
using TestStack.Dossier;

namespace Specs.Acceptance._CurrentSprint.MasterFiles._BaseSpecifications
{
    public abstract class GetOneExisting<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        protected ApiResponse<TDto> Result;
        protected TEntity Existing;

        public virtual void Given_I_have_created_a_master_file()
        {
            Existing = Builder<TEntity>.CreateNew().Persist();
        }

        public async Task When_I_attempt_to_view_it()
        {
            Result = await SUT.GetAsync<TDto>(ApiRoutes.Master.GetFor<TEntity>(Existing.Id));
        }

        public void Then_I_should_see_the_file()
        {
            Result.StatusCode.Should().Be(HttpStatusCode.OK);
            Result.Model.Id.Should().Be(Existing.Id);
        }
    }

    public abstract class GetOneNonExisting<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse<TDto> _result;

        public async Task When_I_attempt_to_view_a_master_file_that_does_not_exist()
        {
            _result = await SUT.GetAsync<TDto>(ApiRoutes.Master.GetFor<TEntity>(99));
        }

        public void Then_I_should_be_warned_that_it_does_not_exist()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}