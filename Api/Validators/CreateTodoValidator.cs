using Api.DTOs;
using Api.UseCases.Todos.Commands;

namespace Api.Validators;

public class CreateTodoValidator : BaseTodoValidator<CreateTodo.Command, CreateTodoDto>
{
    public CreateTodoValidator() : base(x => x.TodoDto)
    {
    }
}