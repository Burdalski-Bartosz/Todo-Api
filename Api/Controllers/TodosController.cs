using Api.Domain.Entities;
using Api.UseCases.Todos.Commands;
using Api.UseCases.Todos.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TodosController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetTodos()
    {
        return await Mediator.Send(new GetTodos.Query());
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetTodo(string id)
    {
        return await Mediator.Send(new GetTodo.Query { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateTodo(Todo todo)
    {
        return await Mediator.Send(new CreateTodo.Command { Todo = todo });
    }

    [HttpPut]
    public async Task<ActionResult> EditTodo(Todo todo)
    {
        await Mediator.Send(new EditTodo.Command { Todo = todo });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTodo(string id)
    {
        await Mediator.Send(new DeleteTodo.Command { Id = id });

        return Ok();
    }
}