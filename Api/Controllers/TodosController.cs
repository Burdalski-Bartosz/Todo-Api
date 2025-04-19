using Api.Db;
using Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public class TodosController(AppDbContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetTodos()
    {
        return await context.Todos.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetTodo(string id)
    {
        var todo = await context.Todos.FindAsync(id);

        if (todo == null) return NotFound();

        return todo;
    }
}