using PersonalBlog.Core.DomainServices;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Application.Services;

/// <summary>
/// здесь добавляем логику из Domain Service из Core. Формируем API для WEB
/// </summary>
public class AppCommentService(CommentService commentService, ICommentRepository commentRepository)
{
    public bool AddComment(Guid userId, Guid articleId, string text)
    {
        if (!commentService.CanUserComment(userId, articleId)) return false;
        commentRepository.AddComment(new Comment {  CommentId = Guid.NewGuid(),
            UserId = userId,
            ArticleId = articleId,
            CommentText = text,
            CreatedAt = DateTime.UtcNow });
        return true;
    }

    public Comment? GetById(Guid id)
    {
        return commentRepository.GetById(id);
    }

    public IEnumerable<Comment> GetAll()
    {
        return commentRepository.GetAll();
    }

    public bool UpdateComment(Guid id, string newText)
    {
        var comment = commentRepository.GetById(id);
        if (comment == null) return false;

        comment.CommentText = newText;
        commentRepository.Update(comment);
        return true;
    }

    public bool DeleteComment(Guid id)
    {
        var comment = commentRepository.GetById(id);
        if (comment == null) return false;

        commentRepository.Delete(comment);
        return true;
    }
}