using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Api.Application.Features.ToDoItems;
using ApiTemplate.Api.Common;
using ApiTemplate.Api.Contracts;
using ApiTemplate.Api.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Api.Controllers
{

    //[Authorize()]
    public class ToDoController : ApiController
    {
        // GET: /todo
        /// <summary>
        /// Gets all the To Do items
        /// </summary>
        [HttpGet(ApiRoutes.ToDo.GetAll)]
        [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ToDoItemDto>>), 200)]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetAll()
        {
            var list = await Mediator.Send(new GetAllToDosQuery());
            return Ok(new SuccessResponse<IEnumerable<ToDoItemDto>>(list));
        }

        // GET: /todo/5
        /// <summary>
        /// Gets a single To Do item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.ToDo.Get)]
        [ProducesResponseType(typeof(SuccessResponse<ToDoItemDto>), 200)]
        public async Task<ActionResult<ToDoItemDto>> Get(int id)
        {
            var result = await Mediator.Send(new GetToDoItemQuery {ToDoItemId = id});

            if (result.IsSuccess)
            {
                return Ok(new SuccessResponse<ToDoItemDto>(result.Value));
            }
            else
            {
                return NotFound();
            }

        }

        // POST: /todo
        /// <summary>
        /// Creates a new To Do item
        /// </summary>
        /// <param name="command"></param>
        [HttpPost(ApiRoutes.ToDo.Create)]
        [ProducesResponseType(typeof(SuccessResponse<RecordsCreatedResponse>), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Create(CreateTodoItemCommand command)
        {
            var result = await Mediator.Send(command);

            return result.ToCreatedResult("ToDo", nameof(Get));
        }

        // PUT: /todo/5
        /// <summary>
        /// Update an existing To Do item
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(ApiRoutes.ToDo.Update)]
        public async Task<IActionResult> Update(UpdateTodoItemCommand command)
        {
            var result = await Mediator.Send(command);

            return result.ToUpdatedResult();
        }

        // DELETE: /todo/5
        /// <summary>
        /// Delete a To Do item
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete(ApiRoutes.ToDo.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteTodoItemCommand { Id = id });

            return result.ToDeletedResult();
        }
    }
}