using System;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Common.Extensions;
using ApiTemplate.Api.Application.Common.Paging;
using ApiTemplate.Api.Application.Features.MasterFiles;
using ApiTemplate.Api.Common;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Common;
using ApiTemplate.Api.Infrastructure.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTemplate.Api.Controllers.MasterFiles
{
    [ApiController]
    [Produces("application/json")]
    public abstract class ReadDeleteControllerFor<TEntity, TDto> : ControllerBase
        where TEntity : Entity
        where TDto : MasterFileDto
    {
        private AppDbContext _context;
        protected AppDbContext Context => _context ??= HttpContext.RequestServices.GetService<AppDbContext>();

        private IMapper _mapper;
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

        public DbSet<TEntity> Entities => Context.Set<TEntity>();

        // GET: master/[controller]
        [HttpGet]
        public virtual async Task<IActionResult> GetAllPaged([FromQuery] PagedQuery query)
        {
            var validationResult = query.IsValid();
            if (validationResult.IsFailed)
            {
                return validationResult.ToFailureResult();
            }

            try
            {
                var data = await Entities.AsQueryable()
                    .AddQuery(query)
                    .ProjectTo<TDto>(Mapper.ConfigurationProvider)
                    .ToListAsync();

                var result = new PagedList<TDto>(query.Page, query.PageSize, data);

                return Ok(new SuccessResponse<PagedList<TDto>>(result));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // GET: master/[controller]/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(int id)
        {
            var dto = await Entities
                .ProjectTo<TDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(new SuccessResponse<TDto>(dto));
        }

        // DELETE: master/[controller]/5
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TEntity>> Delete(int id)
        {
            var entity = await Entities.FindAsync(id);

            if (entity == null)
            {
                return NotFound(new ErrorResponse());
            }

            Entities.Remove(entity);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}