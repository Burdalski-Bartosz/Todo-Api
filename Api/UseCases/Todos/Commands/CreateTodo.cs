using Api.Core;
using Api.Db;
using Api.Domain.Entities;
using Api.DTOs;
using AutoMapper;
using MediatR;

namespace Api.UseCases.Todos.Commands;

public class CreateTodo
{
    public class Command : IRequest<Result<string>>
    {
        public CreateTodoDto TodoDto { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper)
        : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var todo = mapper.Map<Todo>(request.TodoDto);

            context.Todos.Add(todo);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<string>.Failure("Failed to create", 400);

            return Result<string>.Success(todo.Id);
        }
    }
}