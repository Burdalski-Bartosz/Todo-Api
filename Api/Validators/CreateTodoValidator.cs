using Api.UseCases.Todos.Commands;
using FluentValidation;

namespace Api.Validators;

public class CreateTodoValidator : AbstractValidator<CreateTodo.Command>
{
    public CreateTodoValidator()
    {
        RuleFor(x => x.TodoDto.Title).NotEmpty().WithMessage("Title is required");
    }
}