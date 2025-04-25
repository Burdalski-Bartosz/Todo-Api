using Api.Db;
using Api.Domain.Entities;
using MediatR;

namespace Api.UseCases.Todos.Commands;

public class CreateTodo
{
    public class Command : IRequest<string>
    {
        public Todo Todo { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            context.Todos.Add(request.Todo);

            await context.SaveChangesAsync(cancellationToken);

            return request.Todo.Id;
        }
    }
}