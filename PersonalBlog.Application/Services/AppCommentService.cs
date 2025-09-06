using PersobalBlog.Core.Domain_Services;
using PersobalBlog.Core.Repositories;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Application.Services;

public class AppCommentService //здесь добавляем логику из Domain Service из Core. Формируем API для WEB
{
    private readonly CommentService _commentService; //Domain Service
    private readonly ICommentRepository _commentRepository;

    public AppCommentService(CommentService commentService, ICommentRepository commentRepository)
    {
        _commentService = commentService;
        _commentRepository = commentRepository;
    }

    public bool AddComment(Guid userId, Guid articleId, string text)
    {
        if (_commentService.CanUserComment(userId, articleId))
        {
            _commentRepository.AddComment(new Comment {  CommentId = Guid.NewGuid(),
                UserId = userId,
                ArticleId = articleId,
                CommentText = text,
                CreatedAt = DateTime.UtcNow });
            return true;
        }
        return false;
    }

    public Comment? GetById(Guid id)
    {
        return _commentRepository.GetById(id);
    }

    public IEnumerable<Comment> GetAll()
    {
        return _commentRepository.GetAll();
    }

    public bool UpdateComment(Guid id, string newText)
    {
        var comment = _commentRepository.GetById(id);
        if (comment == null) return false;

        comment.CommentText = newText;
        _commentRepository.Update(comment);
        return true;
    }

    public bool DeleteComment(Guid id)
    {
        var comment = _commentRepository.GetById(id);
        if (comment == null) return false;

        _commentRepository.Delete(comment);
        return true;
    }
}