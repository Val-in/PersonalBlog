using PersonalBlog.Core.Interfaces;

namespace PersonalBlog.Core.DomainServices;

/// <summary>
/// Domain Service — бизнес-логика, которая затрагивает несколько сущностей.
/// Бизнес-правило: нельзя комментировать свои статьи
/// </summary>
public class CommentService(IArticleRepository articleRepo)
{
    public bool CanUserComment(Guid userId, Guid articleId)
    {
        var article = articleRepo.GetById(articleId);
        
        return article is { User: not null } && article.User.Id != userId;
    }
}