using System.ComponentModel.DataAnnotations;

namespace Api.DTOs;

public class CreateTodoDto
{
    [Required] public string Title { get; set; } = "";

    public string? Description { get; set; }
    public DateTime Date { get; set; }
}