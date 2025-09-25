using PersonalBlog.Core.Models;

namespace PersonalBlog.Core.Interfaces;

public interface ICommentRepository
{
    void AddComment(Comment comment);
    Comment? GetById(Guid id);
    void Update(Comment comment);
    void Delete(Comment comment);
    public IEnumerable<Comment> GetByArticleId(Guid articleId);
}