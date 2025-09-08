using PersonalBlog.Core.Models;

namespace PersonalBlog.Core.Interfaces;

public interface ICommentRepository
{
    void AddComment(Comment comment);
    Comment? GetById(Guid id);
    IEnumerable<Comment> GetAll();
    void Update(Comment comment);
    void Delete(Comment comment);
}