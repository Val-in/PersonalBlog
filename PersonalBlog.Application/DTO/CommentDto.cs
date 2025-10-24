namespace PersonalBlog.Application.DTO;

public class CommentDto
{
    public Guid Id { get; set; }
    public string? Text { get; set; } = null!;
    public Guid UserId { get; set; }
    public Guid ArticleId { get; set; }
}