namespace PersonalBlog.Application.DTO;

public class ArticleDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public List<TagDto> Tags { get; set; } = new(); 
}