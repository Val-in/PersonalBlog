namespace PersonalBlog.Core.Models;

public class Role
{
    public int RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? Description { get; set; }
    public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
}