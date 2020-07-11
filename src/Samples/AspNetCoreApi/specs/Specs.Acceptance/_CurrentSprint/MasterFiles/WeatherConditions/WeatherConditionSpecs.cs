using System.Collections.Generic;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using AutoMapper;
using FluentAssertions;
using Specs.Acceptance._CurrentSprint.MasterFiles._BaseSpecifications;
using Specs.Library.Builders.Dtos;
using Specs.Library.Builders.Entities.MasterFiles;
using Specs.Library.Data;
using TestStack.Dossier;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.WeatherConditions
{
    public class WeatherConditionStory : MasterFilesStory
    {
        protected override string EntityName => "Weather Condition";
    }

    public class GetOneExistingWeatherCondition : 
        GetOneExisting<WeatherCondition, WeatherConditionDto, WeatherConditionStory>
    {
        public override void Given_I_have_created_a_master_file()
        {
            Existing = WeatherConditionBuilder.WithPersistedType().Persist();
        }

        public void Then_should_have_weather_type()
        {
            Result.Model.WeatherType.Id.Should().Be(Existing.WeatherType.Id);
        }
    }
    public class GetOneNonExistingWeatherCondition : GetOneNonExisting<WeatherCondition, WeatherConditionDto, WeatherConditionStory> { }



    public class GetAllExistingWeatherConditions : GetAllExisting<WeatherCondition, WeatherConditionDto, WeatherConditionStory>
    {
        public override void Given_a_collection_of_TEntitys()
        {
            EntityList = WeatherConditionBuilder
                .CreateListWithPersistedType(3)
                .Persist();
        }
    }
    public class GetAllNonExistingWeatherConditions : GetAllNonExisting<WeatherCondition, WeatherConditionDto, WeatherConditionStory> { }



    public class DeleteOneExistingWeatherCondition 
        : DeleteOneExisting<WeatherCondition, WeatherConditionDto, WeatherConditionStory>
    {
        public override void Given_an_existing_master_file()
        {
            Existing = WeatherConditionBuilder.WithPersistedType().Persist();
        }
    }
    public class DeleteOneNonExistingWeatherCondition : DeleteOneNonExisting<WeatherCondition, WeatherConditionDto, WeatherConditionStory> { }



    public class CreateMultipleInvalidDataWeatherConditions : CreateMultipleInvalidData<WeatherCondition, WeatherConditionDto, WeatherConditionStory> {
        protected override void ModifyDtoToFailValidation(WeatherConditionDto dto)
        {
            dto.Condition = string.Empty;
        }
        protected override ErrorModel CreateErrorModel()
        {
            return new ErrorModel("Condition", "'Condition' must not be empty.");
        }
    }

    public class CreateMultipleValidDataWeatherConditions : CreateMultipleValidData<WeatherCondition,
        WeatherConditionDto, WeatherConditionStory>
    {
        public override void Given_a_set_of_valid_new_Master_Files()
        {
            Command = WeatherConditionDtoBuilder.CreateListWithPersistedType(3);
        }
    }
    public class CreateSingleInvalidDataWeatherCondition : CreateSingleInvalidData<WeatherCondition, WeatherConditionDto, WeatherConditionStory> {
        protected override void ModifyDtoToFailValidation(WeatherConditionDto dto)
        {
            dto.Condition = string.Empty;
        }
        protected override ErrorModel CreateErrorModel()
        {
            return new ErrorModel("Condition", "'Condition' must not be empty.",0);
        }
    }
    public class CreateSingleValidDataWeatherCondition 
        : CreateSingleValidData<WeatherCondition, WeatherConditionDto, WeatherConditionStory>
    {
        public override void Given_a_valid_new_Master_File()
        {
            Command = WeatherConditionDtoBuilder.CreateListWithPersistedType(1);
        }
    }
    public class CreateCannotUpdateWeatherConditions: CreateCannotUpdate<WeatherCondition, WeatherConditionDto, WeatherConditionStory> { }



    public class UpdateMultipleInvalidDataWeatherCondition : UpdateMultipleInvalidData<WeatherCondition, WeatherConditionDto, WeatherConditionStory> {
        protected override List<WeatherCondition> GetExistingEntities()
        {
            return WeatherConditionBuilder.CreateListWithPersistedType(3).Persist();
        }
        protected override void ModifyDtoToFailValidation(WeatherConditionDto dto)
        {
            dto.Condition = string.Empty;
        }
        protected override ErrorModel CreateErrorModel(int rowKey)
        {
            return new ErrorModel("Condition", "'Condition' must not be empty.", rowKey);
        }
    }
    public class UpdateMultipleValidDataWeatherCondition : UpdateMultipleValidData<WeatherCondition, WeatherConditionDto, WeatherConditionStory>
    {
        protected override List<WeatherCondition> GetExistingEntities()
        {
            return WeatherConditionBuilder.CreateListWithPersistedType(3).Persist();
        }
        protected override void ModifyDtoToPassValidation(WeatherConditionDto dto)
        {
            dto.Condition = "Updated" + dto.Id.ToString(); ;
        }
        protected override void VerifyUpdate(WeatherCondition entity)
        {
            entity.Condition.Should().Be("Updated" + entity.Id.ToString());
        }
    }
    public class UpdateNonExistingWeatherConditions : UpdateNonExisting<WeatherCondition, WeatherConditionDto, WeatherConditionStory> { }
    public class UpdateSingleInvalidDataWeatherConditions : UpdateSingleInvalidData<WeatherCondition, WeatherConditionDto, WeatherConditionStory> {
        protected override WeatherCondition GetExistingEntity()
        {
            return WeatherConditionBuilder.WithPersistedType().Persist();
        }
        protected override void ModifyDtoToFailValidation(WeatherConditionDto dto)
        {
            dto.Condition = string.Empty;
        }
        protected override ErrorModel CreateErrorModel(int rowKey)
        {
            return new ErrorModel("Condition", "'Condition' must not be empty.", rowKey);
        }
    }
    public class UpdateSingleValidDataWeatherConditions : UpdateSingleValidData<WeatherCondition, WeatherConditionDto, WeatherConditionStory>
    {
        private WeatherType newWeatherType { get; set; }

        protected override WeatherCondition GetExistingEntity()
        {
            return WeatherConditionBuilder.WithPersistedType().Persist();
        }
        protected override void ModifyDtoToPassValidation(WeatherConditionDto dto)
        {
            newWeatherType = Builder<WeatherType>.CreateNew().Persist();
            var newWeatherTypeDto = Container.Get<IMapper>().Map<WeatherTypeDto>(newWeatherType);
            dto.Condition = "Updated";
            dto.WeatherType = newWeatherTypeDto;
        }
        protected override void VerifyUpdate(WeatherCondition entity)
        {
            entity.Condition.Should().Be("Updated");
            entity.WeatherType.Id.Should().Be(newWeatherType.Id);
        }
    }
    public class UpdateCannotCreateWeatherConditions 
        : UpdateCannotCreate<WeatherCondition, WeatherConditionDto, WeatherConditionStory>
    {
        protected override List<WeatherCondition> GetExistingEntities()
        {
            return WeatherConditionBuilder.CreateListWithPersistedType(3).Persist();
        }
    }
}