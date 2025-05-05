using Api.Core;
using Api.Db;
using Api.Domain.Entities;
using Api.Marker;
using Api.Middleware;
using Api.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class Startup
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssemblyContaining<ApplicationAssemblyMarker>();
                x.AddOpenBehavior(typeof(ValidationBehavior<,>));
            }
        );
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        services.AddValidatorsFromAssemblyContaining<CreateTodoValidator>();
        services.AddTransient<ExceptionMiddleware>();
        services.AddIdentityApiEndpoints<User>(opt => { opt.User.RequireUniqueEmail = true; })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        return services;
    }

    public static IServiceCollection AddWebUiServices(this IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        });
        services.AddCors(); // Konfigurację CORS można też przenieść lub zostawić w Program.cs
        services.AddOpenApi(); // Zakładając, że AddOpenApi to Twoja metoda lub z biblioteki
        // services.AddEndpointsApiExplorer(); // Standardowa rejestracja dla Swaggera
        // services.AddSwaggerGen(); // Standardowa rejestracja dla Swaggera
        return services;
    }

    public static async Task UseDatabaseMigrationAndSeed(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            await context.Database.MigrateAsync();
            await Seed.SeedDatabase(context, userManager); // Używamy istniejącej klasy Seed
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>(); // Można tu użyć generycznego loggera
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            // W środowisku produkcyjnym można rozważyć zatrzymanie aplikacji lub inną logikę
            // throw; // Rzucenie wyjątku dalej może zatrzymać start aplikacji
        }
    }
}