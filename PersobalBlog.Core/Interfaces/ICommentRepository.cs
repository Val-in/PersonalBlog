using PersonalBlog.Core.Models;

namespace PersobalBlog.Core.Repositories;

public interface ICommentRepository
{
    void AddComment(Comment comment);
    Comment? GetById(Guid id);
    IEnumerable<Comment> GetAll();
    void Update(Comment comment);
    void Delete(Comment comment);
}