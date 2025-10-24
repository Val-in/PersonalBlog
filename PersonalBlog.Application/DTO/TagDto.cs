namespace PersonalBlog.Application.DTO;

public class TagDto
{
    public Guid TagId { get; set; }
    public string? TagName { get; set; } = null!;
    public Guid UserId { get; set; }
    public bool? IsPersonal { get; set; }
}