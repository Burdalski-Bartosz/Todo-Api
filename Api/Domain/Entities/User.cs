using Microsoft.AspNetCore.Identity;

namespace Api.Domain.Entities;

public class User : IdentityUser
{
    public string? DisplayName { get; set; }
    public string? Name { get; set; }
}