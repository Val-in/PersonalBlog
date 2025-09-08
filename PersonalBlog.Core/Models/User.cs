using PersonalBlog.Core.Value_Objects;

namespace PersonalBlog.Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string? UserLogin { get; set; }
    public string? Password { get; set; }
    public string? UserNickName { get; set; }
    public DateTime LoginDate { get; set; }
    public Email? Email { get; set; } 

    public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    
    
    
}