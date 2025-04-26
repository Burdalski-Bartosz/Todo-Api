using Api;
using Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPersistenceServices(builder.Configuration)
    .AddApplicationServices()
    .AddWebUiServices();


var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

// app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000"));

app.MapControllers();

await app.UseDatabaseMigrationAndSeed();

app.Run();