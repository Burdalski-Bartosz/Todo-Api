using Api.Db;
using Api.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Api.UseCases.Todos.Commands;

public class EditTodo
{
    public class Command : IRequest
    {
        public required Todo Todo { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var todo = await context.Todos.FindAsync([request.Todo.Id], cancellationToken) ??
                       throw new Exception("Cannot find todo");

            mapper.Map(request.Todo, todo);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}