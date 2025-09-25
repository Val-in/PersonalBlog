namespace PersonalBlog.Application.DTO;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public List<TagDto> Tags { get; set; } = new(); 
}