using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Common;
using FluentAssertions;
using Specify;
using Specify.Stories;
using Specs.Library.Builders;
using Specs.Library.Drivers.Api;
using Specs.Library.Extensions;
using TestStack.Dossier;
using TestStack.Dossier.DataSources.Picking;

namespace Specs.Acceptance._CurrentSprint.MasterFiles._BaseSpecifications
{
    public abstract class CreateMultipleInvalidData<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private List<TDto> _command;
        protected abstract void ModifyDtoToFailValidation(TDto dto);
        protected abstract ErrorModel CreateErrorModel();

        public void Given_an_invalid_list_of_new_Master_Files()
        {
            _command = Builder<TDto>.CreateListOfSize(3)
                .All().Set(x => x.Id, Get.SequenceOf.Keys(3, true).Next);
            _command.ForEach(ModifyDtoToFailValidation);
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(ApiRoutes.Master.CreateFor<TEntity>(), _command);
        }

        public void Then_the_new_file_should_not_be_created()
        {
            Db.Set<TEntity>().Should().HaveCount(0);
        }

        public void And_then_I_should_be_told_the_reasons_why_each_update_with_each_row_identified_by_key()
        {
            _result.Errors.Should().BeEquivalentTo(new List<ErrorModel>()
            {
                CreateErrorModel().With(x => x.RowKey = 0),
                CreateErrorModel().With(x => x.RowKey = -1),
                CreateErrorModel().With(x => x.RowKey = -2)
            });
        }
    }

    public abstract class CreateMultipleValidData<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        protected List<TDto> Command;

        public virtual void Given_a_set_of_valid_new_Master_Files()
        {
            Command = Builder<TDto>.CreateListOfSize(3)
                .All()
                .Set(x => x.Id, 0);
        }

        public async Task When_I_attempt_to_create_them()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(
                ApiRoutes.Master.CreateFor<TEntity>(), Command);
        }

        public void Then_the_new_files_should_be_created()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.Created);
            _result.Model.NewIds[0].Should().BeGreaterThan(0);

            Db.Set<TEntity>().Should().HaveCount(3);
        }

        public void AndThen_the_link_to_the_first_new_file_should_be_provided()
        {
            _result.Headers.Location.AbsolutePath
                .Should().Be(ApiRoutes.Master.GetFor<TEntity>(_result.Model.NewIds[0]));
        }
    }

    public abstract class CreateSingleInvalidData<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto, new()
        where TStory : Story, new()
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private List<TDto> _command;

        protected abstract void ModifyDtoToFailValidation(TDto dto);
        protected abstract ErrorModel CreateErrorModel();

        public void Given_an_invalid_new_Master_File()
        {
            _command = Builder<TDto>.CreateListOfSize(1)
                .All().Set(x => x.Id, 0);
            _command.ForEach(ModifyDtoToFailValidation);
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(ApiRoutes.Master.CreateFor<TEntity>(), _command);
        }

        public void Then_the_new_file_should_not_be_created()
        {
            Db.Set<TEntity>().Should().HaveCount(0);
        }

        public void And_then_I_should_be_told_the_reasons_why()
        {
            var errorModel = CreateErrorModel();
            _result.Errors[0].Should().BeEquivalentTo(errorModel);
        }
    }

    public abstract class CreateSingleValidData<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        protected List<TDto> Command;

        public virtual void Given_a_valid_new_Master_File()
        {
            Command = Builder<TDto>.CreateListOfSize(1)
                .All().Set(x => x.Id, 0);
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(
                ApiRoutes.Master.CreateFor<TEntity>(), Command);
        }

        public void Then_the_new_file_should_be_created()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.Created);
            _result.Model.NewIds[0].Should().BeGreaterThan(0);
            Db.Find<TEntity>(_result.Model.NewIds[0]).Should().NotBeNull();
        }

        public void AndThen_the_link_to_the_new_file_should_be_provided()
        {
            _result.Headers.Location.AbsolutePath
                .Should().Be(ApiRoutes.Master.GetFor<TEntity>(_result.Model.NewIds[0]));
        }
    }

    public abstract class CreateCannotUpdate<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private List<TDto> _command;

        public void Given_some_new_Master_Files_have_Id_greater_than_zero()
        {
            var keys = new List<int> { 0, 999, -2 }; // 999 is an invalid key because it's not less than or equal to zero
            _command = Builder<TDto>.CreateListOfSize(3)
                .All()
                .Set(x => x.Id, Pick.RepeatingSequenceFrom(keys).Next);
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(ApiRoutes.Master.CreateFor<TEntity>(), _command);
        }

        public void Then_the_new_file_should_not_be_created()
        {
            Db.Set<TEntity>().Should().HaveCount(0);
        }

        public void And_then_I_should_be_told_the_reasons_why_each_update_with_each_row_identified_by_key()
        {
            _result.ShouldContainErrors(
                "Id must be less than or equal to zero. Use the Update endpoint for updating existing records.");
        }
    }

}