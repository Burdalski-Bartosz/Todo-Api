using Api.Core;
using Api.Db;
using MediatR;

namespace Api.UseCases.Todos.Commands;

public class DeleteTodo
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var todo = await context.Todos.FindAsync([request.Id], cancellationToken);

            if (todo == null) return Result<Unit>.Failure("Todo not found", 404);

            context.Remove(todo);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to delete", 400);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}