using Api.DTOs;
using Api.UseCases.Todos.Commands;
using FluentValidation;

namespace Api.Validators;

public class EditTodoValidator : BaseTodoValidator<EditTodo.Command, EditTodoDto>
{
    public EditTodoValidator() : base(x => x.TodoDto)
    {
        RuleFor(x => x.TodoDto.Id).NotEmpty().WithMessage("Id is required");
    }
}