using Api.Db;
using Api.Domain.Entities;
using MediatR;

namespace Api.UseCases.Todos.Queries;

public class GetTodo
{
    public class Query : IRequest<Todo>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Todo>
    {
        public async Task<Todo> Handle(Query request, CancellationToken cancellationToken)
        {
            var todo = await context.Todos.FindAsync([request.Id], cancellationToken);

            if (todo == null) throw new Exception("Todo not found");

            return todo;
        }
    }
}