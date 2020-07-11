using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Common;
using AutoMapper;
using FluentAssertions;
using Specify.Stories;
using Specs.Library.Builders;
using Specs.Library.Data;
using Specs.Library.Drivers.Api;
using TestStack.Dossier;

namespace Specs.Acceptance._CurrentSprint.MasterFiles._BaseSpecifications
{
    public abstract class UpdateCannotCreate<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse _result;
        private List<TEntity> _existingItems;
        private List<TDto> _updates;

        public void Given_I_am_updating_Master_Files_and_one_has_Id_less_than_zero()
        {
            _existingItems = GetExistingEntities();
            _updates = Container.Get<IMapper>()
                .Map<List<TDto>>(_existingItems);
            _updates[2].Id = -1;
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.Master.UpdateFor<TEntity>(), _updates);
        }

        public void Then_the_changes_should_not_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            foreach (var entity in Db.Set<TEntity>())
            {
                entity.Should().Be(_existingItems.Single(x => x.Id == entity.Id));
            }
        }

        public void And_then_I_should_be_told_the_reasons_why()
        {
            _result.Errors[0].Message.Should().Be("Id must be greater than zero. Use the Create endpoint for new records.");
            _result.Errors.Count.Should().Be(1);
        }

        protected virtual List<TEntity> GetExistingEntities()
        {
            return Builder<TEntity>.CreateListOfSize(3).Persist();
        }
    }

    public abstract class UpdateMultipleInvalidData<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse _result;
        private List<TEntity> _existingItems;
        private List<TDto> _updates;

        protected abstract void ModifyDtoToFailValidation(TDto dto);
        protected abstract ErrorModel CreateErrorModel(int rowKey);

        public void Given_I_have_made_invalid_changes_to_multiple_existing_items()
        {
            _existingItems = GetExistingEntities();
            _updates = Container.Get<IMapper>()
                .Map<List<TDto>>(_existingItems);
            _updates.ForEach(ModifyDtoToFailValidation);
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.Master.UpdateFor<TEntity>(), _updates);
        }

        public void Then_the_changes_should_not_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            foreach (var entity in Db.Set<TEntity>())
            {
                entity.Should().Be(_existingItems.Single(x => x.Id == entity.Id));
            }
        }

        public void And_then_I_should_be_told_the_reasons_why()
        {
            _result.Errors.Should().BeEquivalentTo(new List<ErrorModel>()
            {
                CreateErrorModel(_existingItems[0].Id),
                CreateErrorModel(_existingItems[1].Id),
                CreateErrorModel(_existingItems[2].Id)
            });
        }

        protected virtual List<TEntity> GetExistingEntities()
        {
            return Builder<TEntity>.CreateListOfSize(3).Persist();
        }
    }

    public abstract class UpdateMultipleValidData<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse _result;
        private List<TEntity> _existingItems;
        private List<TDto> _updates;

        protected abstract void ModifyDtoToPassValidation(TDto dto);
        protected abstract void VerifyUpdate(TEntity entity);

        public void Given_I_have_made_valid_changes_to_multiple_existing_items()
        {
            _existingItems = GetExistingEntities();
            _updates = Container.Get<IMapper>()
                .Map<List<TDto>>(_existingItems);
            _updates.ForEach(ModifyDtoToPassValidation);
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.Master.UpdateFor<TEntity>(), _updates);
        }

        public void Then_the_changes_should_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);

            foreach (var entity in Db.Set<TEntity>())
            {
                VerifyUpdate(entity);
            }
        }
        protected virtual List<TEntity> GetExistingEntities()
        {
            return Builder<TEntity>.CreateListOfSize(3).Persist();
        }
    }

    public abstract class UpdateNonExisting<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse _result;
        private List<TDto> _updates;

        public void Given_I_am_trying_to_edit_a_file_that_does_not_exist()
        {
            _updates = new List<TDto> { Get.InstanceOf<TDto>() };
        }

        public async Task When_I_attempt_to_apply_changes_to_it()
        {
            _result = await SUT.PutAsync(ApiRoutes.Master.UpdateFor<TEntity>(), _updates);
        }

        public void Then_I_should_be_warned_that_the_item_does_not_exist()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }

    public abstract class UpdateSingleInvalidData<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse _result;
        private TEntity _existingItem;
        private List<TDto> _updates;

        protected abstract void ModifyDtoToFailValidation(TDto dto);
        protected abstract ErrorModel CreateErrorModel(int rowKey);

        public void Given_I_have_made_invalid_changes_to_an_existing_item()
        {
            _existingItem = GetExistingEntity();
            _updates = Container.Get<IMapper>()
                .Map<List<TDto>>(new List<TEntity>{_existingItem});
            _updates.ForEach(ModifyDtoToFailValidation);
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.Master.UpdateFor<TEntity>(), _updates);
        }

        public void Then_the_changes_should_not_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            Db.Set<TEntity>().Find(_existingItem.Id).Should().Be(_existingItem);
        }

        public void And_then_I_should_be_told_the_reasons_why()
        {
            var expectedError = CreateErrorModel(_existingItem.Id);
            _result.Errors[0].Should().BeEquivalentTo(expectedError);
        }
        protected virtual TEntity GetExistingEntity()
        {
            return Builder<TEntity>.CreateNew().Persist();
        }
    }

    public abstract class UpdateSingleValidData<TEntity, TDto, TStory> : ScenarioFor<AsyncApiDriver, TStory>
        where TEntity : Entity
        where TDto : MasterFileDto
        where TStory : Story, new()
    {
        private ApiResponse _result;
        private TEntity _existingItem;
        private List<TDto> _updates;

        protected abstract void ModifyDtoToPassValidation(TDto dto);
        protected abstract void VerifyUpdate(TEntity entity);

        public void Given_I_have_made_valid_changes_to_one_existing_item()
        {
            _existingItem = GetExistingEntity();
            _updates = Container.Get<IMapper>()
                .Map<List<TDto>>(new List<TEntity> { _existingItem });
            _updates.ForEach(ModifyDtoToPassValidation);
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.Master.UpdateFor<TEntity>(), _updates);
        }

        public void Then_the_changes_should_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
            VerifyUpdate(Db.Find<TEntity>(_existingItem.Id));
        }

        protected virtual TEntity GetExistingEntity()
        {
            return Builder<TEntity>.CreateNew().Persist();
        }
    }
}
