using PersonalBlog.Application.DTO;
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

    public IEnumerable<CommentDto> GetByArticleId(Guid articleId)
    {
        return commentRepository.GetByArticleId(articleId)
            .Select(c => new CommentDto
            {
                Id = c.CommentId,
                Text = c.CommentText,
                UserId = c.UserId,
                ArticleId = c.ArticleId
            })
            .ToList();
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

    public CommentDto? GetById(Guid id)
    {
        var comment = commentRepository.GetById(id);
        if (comment == null) return null;

        return new CommentDto
        {
            Id = comment.CommentId,
            Text = comment.CommentText,
            UserId = comment.UserId,
            ArticleId = comment.ArticleId
        };
    }
}