namespace PersonalBlog.Core.Models;

public class Tag
{
    public int Id { get; set; }
    public string? TagName { get; set; }
    public ArticleTag? ArticleTagName { get; set; }
}