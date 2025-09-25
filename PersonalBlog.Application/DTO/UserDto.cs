using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Application.DTO;

public class UserDto
{
    public string? Nickname { get; set; }
    [Required]
    public string? Login { get; set; }
    [Required]
    public string? Password { get; set; }
}