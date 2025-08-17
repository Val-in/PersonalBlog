namespace PersonalBlog.Core.Models;

public class Article
{
    public Guid ArticleId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public User? User { get; set; }
    public ArticleTag? ArticleTagName { get; set; }
}