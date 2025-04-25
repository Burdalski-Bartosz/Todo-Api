using Api.Db;
using Api.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.UseCases.Todos.Queries;

public class GetTodos
{
    public class Query : IRequest<List<Todo>>
    {
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, List<Todo>>
    {
        public async Task<List<Todo>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Todos.ToListAsync(cancellationToken);
        }
    }
}