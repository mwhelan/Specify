using ApiTemplate.Api.Common;
using ApiTemplate.Api.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Behaviours;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Domain.Model.MasterFiles;
using ApiTemplate.Api.Domain.Utils;

namespace ApiTemplate.Api.Controllers.MasterFiles
{
    public class WeatherConditionController : MasterFileControllerFor<WeatherCondition, WeatherConditionDto>
    {
        // POST: master/[controller]
        [HttpPost]
        public override async Task<IActionResult> Create(List<WeatherConditionDto> dtoList)
        {
            if (dtoList.Any(x => x.Id > 0))
            {
                return ActionResults.ValidationFailure("Id",
                    "Id must be less than or equal to zero. Use the Update endpoint for updating existing records.");
            }

            var result = ValidateModel(dtoList);
            if (result.IsFailed)
            {
                return result.ToFailureResult();
            }

            var entities = Mapper.Map<WeatherCondition[]>(dtoList);
            entities.Each(x => {
                x.Id = 0;           //TODO: Do we want to make the Id protected.
                x.WeatherType = Context.WeatherTypes.Find(x.WeatherType.Id);
                });   
            Context.AddRange(entities);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                Log.Error(exception, "Database update exception.");
                var failResult = DbUtil.HandleDatabaseException(exception, typeof(WeatherCondition).Name);
                return failResult.ToFailureResult();
            }

            var newIds = entities.Select(x => x.Id).ToList();
            var response = new SuccessResponse<RecordsCreatedResponse>(
                new RecordsCreatedResponse(newIds), "Records created");
            return CreatedAtAction(nameof(Get), new { id = entities[0].Id }, response);
        }

        // PUT: master/[controller]/5
        [HttpPut]
        public override async Task<IActionResult> Update(List<WeatherConditionDto> dtoList)
        {
            if (dtoList.Any(x => x.Id <= 0))
            {
                return ActionResults.ValidationFailure("Id",
                    "Id must be greater than zero. Use the Create endpoint for new records.");
            }

            var result = ValidateModel(dtoList);
            if (result.IsFailed)
            {
                return result.ToFailureResult();
            }

            var allEntitiesExistInDb = await EntitiesExist(dtoList.Select(x => x.Id).ToList());
            if (!allEntitiesExistInDb)
            {
                return NotFound();
            }

            var entities = Mapper.Map<WeatherCondition[]>(dtoList);
            entities.Each(x => x.WeatherType = Context.WeatherTypes.Find(x.WeatherType.Id));
            Context.UpdateRange(entities);

            await Context.SaveChangesAsync();

            return Ok();
        }
    }
}
