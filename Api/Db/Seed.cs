using Api.Domain.Entities;

namespace Api.Db;

public class Seed
{
    public static async Task SeedDatabase(AppDbContext context)
    {
        if (context.Todos.Any()) return;

        var todos = new List<Todo>
        {
            new()
            {
                Title = "Add Item 1",
                Description = "Lorem ipsum 1",
                Date = DateTime.Now
            },
            new()
            {
                Title = "Add Item 2",
                Description = "Lorem ipsum 2",
                Date = DateTime.Now
            },
            new()
            {
                Title = "Add Item 3",
                Description = "Lorem ipsum 3",
                Date = DateTime.Now
            },
            new()
            {
                Title = "Add Item 4",
                Description = "Lorem ipsum 4",
                Date = DateTime.Now
            }
        };

        context.Todos.AddRange(todos);

        await context.SaveChangesAsync();
    }
}