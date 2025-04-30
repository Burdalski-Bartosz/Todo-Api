using Api.Domain.Entities;
using Api.DTOs;
using Api.UseCases.Todos.Commands;
using Api.UseCases.Todos.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TodosController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetTodos()
    {
        return await Mediator.Send(new GetTodos.Query());
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetTodo(string id)
    {
        return HandleResult(await Mediator.Send(new GetTodo.Query { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateTodo(CreateTodoDto todoDto)
    {
        return HandleResult(await Mediator.Send(new CreateTodo.Command { TodoDto = todoDto }));
    }

    [HttpPut]
    public async Task<ActionResult> EditTodo(EditTodoDto todo)
    {
        return HandleResult(await Mediator.Send(new EditTodo.Command { TodoDto = todo }));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTodo(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteTodo.Command { Id = id }));
    }
}