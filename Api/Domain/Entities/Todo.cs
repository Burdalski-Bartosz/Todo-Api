namespace Api.Domain.Entities;

public class Todo
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}