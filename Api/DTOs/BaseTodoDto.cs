namespace Api.DTOs;

public class BaseTodoDto
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}