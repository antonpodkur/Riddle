using Microsoft.AspNetCore.Identity;

namespace webapi.Models;

public class User : IdentityUser<Guid>
{
    public Game? Game { get; set; }
}