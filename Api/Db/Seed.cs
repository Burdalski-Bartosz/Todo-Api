using Api.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Api.Db;

public class Seed
{
    public static async Task SeedDatabase(AppDbContext context, UserManager<User> userManager)
    {
        var users = new List<User>
        {
            new()
            {
                Id = "admin-1",
                Email = "admin1@admin.com",
                UserName = "admin1@admin1.com"
            },
            new()
            {
                Id = "admin-2",
                Email = "admin2@admin.com",
                UserName = "admin2@admin2.com"
            },
            new()
            {
                Id = "admin-3",
                Email = "admin3@admin.com",
                UserName = "admin3@admin3.com"
            }
        };

        if (!userManager.Users.Any())
            foreach (var user in users)
            {
                Console.WriteLine(user.UserName);
                await userManager.CreateAsync(user, "Admin123!!");
            }

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