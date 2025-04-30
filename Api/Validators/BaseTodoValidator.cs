using Api.DTOs;
using FluentValidation;

namespace Api.Validators;

public class BaseTodoValidator<T, TDto> : AbstractValidator<T>
    where TDto : BaseTodoDto
{
    public BaseTodoValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Title).NotEmpty().WithMessage("Title is required");
    }
}