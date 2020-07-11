using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using FluentAssertions;
using Specs.Acceptance._CurrentSprint.MasterFiles._BaseSpecifications;

namespace Specs.Acceptance._CurrentSprint.MasterFiles.WeatherTypes
{
    public class WeatherTypeStory : MasterFilesStory
    {
        protected override string EntityName => "Weather Type";
    }

    public class GetOneExistingWeatherType : GetOneExisting<WeatherType, WeatherTypeDto, WeatherTypeStory> { }
    public class GetOneNonExistingWeatherType : GetOneNonExisting<WeatherType, WeatherTypeDto, WeatherTypeStory> { }



    public class GetAllExistingWeatherTypes : GetAllExisting<WeatherType, WeatherTypeDto, WeatherTypeStory> { }
    public class GetAllNonExistingWeatherTypes : GetAllNonExisting<WeatherType, WeatherTypeDto, WeatherTypeStory> { }



    public class DeleteOneExistingWeatherType : DeleteOneExisting<WeatherType, WeatherTypeDto, WeatherTypeStory> { }
    public class DeleteOneNonExistingWeatherType : DeleteOneNonExisting<WeatherType, WeatherTypeDto, WeatherTypeStory> { }



    public class CreateMultipleInvalidDataWeatherTypes : CreateMultipleInvalidData<WeatherType, WeatherTypeDto, WeatherTypeStory> {
        protected override void ModifyDtoToFailValidation(WeatherTypeDto dto)
        {
            dto.WeatherTypeName = string.Empty;
        }
        protected override ErrorModel CreateErrorModel()
        {
            return new ErrorModel("WeatherTypeName", "'Weather Type Name' must not be empty.");
        }
    }
    public class CreateMultipleValidDataWeatherTypes : CreateMultipleValidData<WeatherType, WeatherTypeDto, WeatherTypeStory> { }
    public class CreateSingleInvalidDataWeatherType : CreateSingleInvalidData<WeatherType, WeatherTypeDto, WeatherTypeStory> {
        protected override void ModifyDtoToFailValidation(WeatherTypeDto dto)
        {
            dto.WeatherTypeName = string.Empty;
        }
        protected override ErrorModel CreateErrorModel()
        {
            return new ErrorModel("WeatherTypeName", "'Weather Type Name' must not be empty.",0);
        }
    }
    public class CreateSingleValidDataWeatherType : CreateSingleValidData<WeatherType, WeatherTypeDto, WeatherTypeStory> { }
    public class CreateCannotUpdateWeatherTypes: CreateCannotUpdate<WeatherType, WeatherTypeDto, WeatherTypeStory> { }



    public class UpdateMultipleInvalidDataWeatherType : UpdateMultipleInvalidData<WeatherType, WeatherTypeDto, WeatherTypeStory> {
        protected override void ModifyDtoToFailValidation(WeatherTypeDto dto)
        {
            dto.WeatherTypeName = string.Empty;
        }
        protected override ErrorModel CreateErrorModel(int rowKey)
        {
            return new ErrorModel("WeatherTypeName", "'Weather Type Name' must not be empty.", rowKey);
        }
    }
    public class UpdateMultipleValidDataWeatherType : UpdateMultipleValidData<WeatherType, WeatherTypeDto, WeatherTypeStory>
    {
        protected override void ModifyDtoToPassValidation(WeatherTypeDto dto)
        {
            dto.WeatherTypeName = "Updated" + dto.Id.ToString();
        }
        protected override void VerifyUpdate(WeatherType entity)
        {
            entity.WeatherTypeName.Should().Be("Updated" + entity.Id.ToString());
        }
    }
    public class UpdateNonExistingWeatherTypes : UpdateNonExisting<WeatherType, WeatherTypeDto, WeatherTypeStory> { }
    public class UpdateSingleInvalidDataWeatherTypes : UpdateSingleInvalidData<WeatherType, WeatherTypeDto, WeatherTypeStory> {
        protected override void ModifyDtoToFailValidation(WeatherTypeDto dto)
        {
            dto.WeatherTypeName = string.Empty;
        }
        protected override ErrorModel CreateErrorModel(int rowKey)
        {
            return new ErrorModel("WeatherTypeName", "'Weather Type Name' must not be empty.", rowKey);
        }
    }
    public class UpdateSingleValidDataWeatherTypes : UpdateSingleValidData<WeatherType, WeatherTypeDto, WeatherTypeStory>
    {
        protected override void ModifyDtoToPassValidation(WeatherTypeDto dto)
        {
            dto.WeatherTypeName = "Updated";
        }
        protected override void VerifyUpdate(WeatherType entity)
        {
            entity.WeatherTypeName.Should().Be("Updated");
        }
    }
    public class UpdateCannotCreateWeatherTypes : UpdateCannotCreate<WeatherType, WeatherTypeDto, WeatherTypeStory> { }
}