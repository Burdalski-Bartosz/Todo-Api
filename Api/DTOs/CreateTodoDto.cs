namespace Api.DTOs;

public class CreateTodoDto
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}