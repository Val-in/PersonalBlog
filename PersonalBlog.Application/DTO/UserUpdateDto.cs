using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Application.DTO;

public class UserUpdateDto
{
    [Required]
    public string Login { get; set; } = null!;
    [Required]
    public string Nickname { get; set; } = null!;
    [Required]
    public string? Password { get; set; } 
    public int? RoleId { get; set; }    
}