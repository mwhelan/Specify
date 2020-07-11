using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Paging;
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
    public abstract class GetAllNonExisting<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse<PagedList<TDto>> _result;

        public void Given_there_are_no_files()
        {
        }

        public async Task When_I_view_the_list()
        {
            _result = await SUT.GetAllPagedAsync<TDto>(ApiRoutes.Master.GetAllFor<TEntity>());
        }

        public void Then_I_should_see_an_empty_list()
        {
            _result.Model.Results.Count.Should().Be(0);
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }

    public abstract class GetAllExisting<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse<PagedList<TDto>> _result;
        protected IList<TEntity> EntityList;

        public virtual void Given_a_collection_of_TEntitys()
        {
            EntityList = Builder<TEntity>.CreateListOfSize(3)
                .All()
                .Set(x => x.Id, 0)
                .ListBuilder
                .Persist();
        }

        public async Task When_I_view_my_list()
        {
            _result = await SUT.GetAllPagedAsync<TDto>(ApiRoutes.Master.GetAllFor<TEntity>());
        }

        public void Then_I_should_see_the_whole_collection()
        {
            _result.Model.Results.Count.Should().Be(EntityList.Count);
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }

}