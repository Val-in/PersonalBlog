namespace PersonalBlog.Core.Models;

public class Comment
{
    public int CommentId { get; set; }
    public string CommentText { get; set; }
    public Article ArticleId { get; set; }
    public User UserId { get; set; }
    
}