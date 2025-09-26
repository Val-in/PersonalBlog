using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Application.DTO;

public class LoginDto
{
    [Required]
    public string? Login { get; set; }
    [Required]
    public string? Password { get; set; }
}