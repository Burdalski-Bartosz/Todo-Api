using Api.Core;
using Api.Db;
using Api.DTOs;
using AutoMapper;
using MediatR;

namespace Api.UseCases.Todos.Commands;

public class EditTodo
{
    public class Command : IRequest<Result<Unit>>
    {
        public required EditTodoDto TodoDto { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var todo = await context.Todos.FindAsync([request.TodoDto.Id], cancellationToken);

            if (todo == null) return Result<Unit>.Failure("Todo not found", 404);

            mapper.Map(request.TodoDto, todo);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to edit", 400);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}