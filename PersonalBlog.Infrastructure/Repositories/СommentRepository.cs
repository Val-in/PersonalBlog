using PersobalBlog.Core.Repositories;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void AddComment(Comment comment)
    {
        comment.CommentText = comment.CommentText.Trim();
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }

    public Comment? GetById(Guid id) => _context.Comments.Find(id);

    public IEnumerable<Comment> GetAll() => _context.Comments.ToList();

    public void Update(Comment comment)
    {
        _context.Comments.Update(comment);
        _context.SaveChanges();
    }

    public void Delete(Comment comment)
    {
        if (comment != null)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}