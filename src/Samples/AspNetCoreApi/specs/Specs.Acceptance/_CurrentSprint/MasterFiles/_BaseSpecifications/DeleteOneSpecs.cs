using System.Collections.Generic;
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
    public abstract class DeleteOneExisting<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse _result;
        protected TEntity Existing;
        private List<TDto> _updates;

        public virtual void Given_an_existing_master_file()
        {
            Existing = Builder<TEntity>.CreateNew().Persist();
        }

        public async Task When_I_attempt_to_delete_it()
        {
            _result = await SUT.DeleteAsync(ApiRoutes.Master.DeleteFor<TEntity>(Existing.Id));
        }

        public void Then_the_file_should_be_deleted()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            Db.Set<TEntity>().Should().HaveCount(0);
        }
    }

    public abstract class DeleteOneNonExisting<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse _result;

        public void Given_I_am_trying_to_delete_a_file_that_does_not_exist()
        {
        }

        public async Task When_I_attempt_to_delete_it()
        {
            _result = await SUT.DeleteAsync(ApiRoutes.Master.DeleteFor<TEntity>(99));
        }

        public void Then_I_should_be_warned_that_the_file_does_not_exist()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }

}