namespace PersonalBlog.Core.Models;

public class UserRoles
{
    // составной ключ: UserId + RoleId
    public int UserId { get; set; }
    public User User { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }
}