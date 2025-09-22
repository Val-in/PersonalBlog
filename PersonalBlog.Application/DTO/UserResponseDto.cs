namespace PersonalBlog.Application.DTO;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public List<string> Roles { get; set; } = new();
    public string Message { get; set; } = null!;
}