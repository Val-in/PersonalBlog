namespace PersonalBlog.Core.Models;

public class Tag
{
    public Guid TagId { get; set; }
    public string? TagName { get; set; }
    public Guid? UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public User? User { get; set; }
    public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
}