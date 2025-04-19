using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Db;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<Todo> Todos { get; set; }
}