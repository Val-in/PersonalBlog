namespace PersonalBlog.Core.Models;

public class Article
{
    public Guid ArticleId { get; set; }
    public Guid AuthorId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public User? User { get; set; }
    public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
}