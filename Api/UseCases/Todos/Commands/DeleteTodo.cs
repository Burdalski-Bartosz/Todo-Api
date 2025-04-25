using Api.Db;
using MediatR;

namespace Api.UseCases.Todos.Commands;

public class DeleteTodo
{
    public class Command : IRequest
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var todo = await context.Todos.FindAsync([request.Id], cancellationToken) ??
                       throw new Exception("Cannot find todo");

            context.Remove(todo);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}