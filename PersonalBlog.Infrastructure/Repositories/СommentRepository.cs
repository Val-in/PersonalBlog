using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Infrastructure.Repositories;

public class CommentRepository(AppDbContext context) : ICommentRepository
{
    public void AddComment(Comment comment)
    {
        comment.CommentText = comment.CommentText?.Trim();
        context.Comments.Add(comment);
        context.SaveChanges();
    }

    public Comment? GetById(Guid id) => context.Comments.Find(id);
    
    // В репозитории
    public IEnumerable<Comment> GetByArticleId(Guid articleId)
    {
        return context.Comments.Where(c => c.ArticleId == articleId).ToList();
    }

    public void Update(Comment comment)
    {
        context.Comments.Update(comment);
        context.SaveChanges();
    }

    public void Delete(Comment comment)
    {
        context.Comments.Remove(comment);
        context.SaveChanges();
    }
}