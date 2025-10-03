namespace PersonalBlog.Application.DTO;

public class ArticleDto
{
    public Guid ArticleId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string AuthorName { get; set; } = null!;
    public List<TagDto> Tags { get; set; } = new(); 
}