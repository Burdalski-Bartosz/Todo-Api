using Api.Core;
using Api.Db;
using Api.Marker;
using Api.Validators;
using FluentValidation;
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

        return services;
    }

    public static IServiceCollection AddWebUiServices(this IServiceCollection services)
    {
        services.AddControllers();
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
            await context.Database.MigrateAsync();
            await Seed.SeedDatabase(context); // Używamy istniejącej klasy Seed
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