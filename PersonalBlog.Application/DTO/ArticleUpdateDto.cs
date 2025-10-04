namespace PersonalBlog.Application.DTO;

public class ArticleUpdateDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public List<Guid> Tags { get; set; } = new();
}