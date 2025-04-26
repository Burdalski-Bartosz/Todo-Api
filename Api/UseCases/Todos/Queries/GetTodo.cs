using Api.Core;
using Api.Db;
using Api.Domain.Entities;
using MediatR;

namespace Api.UseCases.Todos.Queries;

public class GetTodo
{
    public class Query : IRequest<Result<Todo>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Result<Todo>>
    {
        public async Task<Result<Todo>> Handle(Query request, CancellationToken cancellationToken)
        {
            var todo = await context.Todos.FindAsync([request.Id], cancellationToken);

            if (todo == null) return Result<Todo>.Failure("Todo not found", 404);

            return Result<Todo>.Success(todo);
        }
    }
}