using Api.Db;
using Api.Domain.Entities;
using Api.DTOs;
using AutoMapper;
using MediatR;

namespace Api.UseCases.Todos.Commands;

public class CreateTodo
{
    public class Command : IRequest<string>
    {
        public CreateTodoDto TodoDto { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var todo = mapper.Map<Todo>(request.TodoDto);
            context.Todos.Add(todo);

            await context.SaveChangesAsync(cancellationToken);

            return todo.Id;
        }
    }
}