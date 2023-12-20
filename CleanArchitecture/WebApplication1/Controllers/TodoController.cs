using Application.Common.Models;
using Application.TodoItems.Queries.GetTodoItemsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ISender _sender;
        public TodoController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetTodoItems")]
        public async Task<PaginatedList<TodoItemBriefDto>> GetTodoItemsWithPagination(
            [FromQuery] GetTodoItemsWithPaginationQuery query)
        => await _sender.Send(query);
    }
}
