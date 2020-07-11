using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Behaviours;
using ApiTemplate.Api.Application.Common.Validation;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Common;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Common;
using ApiTemplate.Api.Domain.Utils;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ApiTemplate.Api.Controllers.MasterFiles
{
    [Route("master/[controller]")]
    public abstract class MasterFileControllerFor<TEntity, TDto> : ReadDeleteControllerFor<TEntity, TDto>
        where TEntity : MasterFile
        where TDto : MasterFileDto
    {
        private IEnumerable<IValidator<TDto>> _validators;
        protected IEnumerable<IValidator<TDto>> Validators =>
            _validators ??= HttpContext.RequestServices.GetServices<IValidator<TDto>>();

        // POST: master/[controller]
        [HttpPost]
        public virtual async Task<IActionResult> Create(List<TDto> dtoList)
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

            var entities = Mapper.Map<TEntity[]>(dtoList);
            entities.Each(x => x.Id = 0);   //TODO: Do we want to make the Id protected.
            Context.AddRange(entities);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                Log.Error(exception, "Database update exception.");
                var failResult = DbUtil.HandleDatabaseException(exception, typeof(TEntity).Name);
                return failResult.ToFailureResult();
            }

            var newIds = entities.Select(x =>x.Id).ToList();
            var response = new SuccessResponse<RecordsCreatedResponse>(
                new RecordsCreatedResponse(newIds), "Records created");
            return CreatedAtAction(nameof(Get), new {id = entities[0].Id}, response);
        }

        // PUT: master/[controller]/5
        [HttpPut]
        public virtual async Task<IActionResult> Update(List<TDto> dtoList)
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

            var entities = Mapper.Map<TEntity[]>(dtoList);
            Context.UpdateRange(entities);

            //Context.Attach(entity);
            //Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync();

            return Ok();
        }

        protected async Task<bool> EntitiesExist(List<int> idList)
        {
            return (await Entities.AsNoTracking()
                    .Select(x => x.Id)
                    .CountAsync(id => idList.Contains(id))
                ) == idList.Distinct().Count();
        }

        protected Result ValidateModel(List<TDto> list)
        {
            var result = Results.Ok();

            if (!Validators.Any())
            {
                return result;
            }

            foreach (var dto in list)
            {
                var context = new ValidationContext<TDto>(dto);

                var failures = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    result.WithValidationResults(failures, dto.Id);
                }
            }

            if (result.Reasons.Count > 0)
            {
                Log.Warning(
                    "One or more validation failures have occurred.: {Name} {@ValidationErrors} {@Request}",
                    typeof(TDto).Name, result.Reasons, list);
            }

            return result;
        }
    }
}