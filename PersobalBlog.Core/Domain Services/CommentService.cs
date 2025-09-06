using PersobalBlog.Core.Repositories;

namespace PersobalBlog.Core.Domain_Services;

public class CommentService //Domain Service — бизнес-логика, которая затрагивает несколько сущностей.
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;

    public CommentService(IUserRepository userRepo, IArticleRepository articleRepo)
    {
        _userRepository = userRepo;
        _articleRepository = articleRepo;
    }

    public bool CanUserComment(Guid userId, Guid articleId)
    {
        var user = _userRepository.GetById(userId);
        var article = _articleRepository.GetById(articleId);

        // пример бизнес-правила: нельзя комментировать свои статьи
        return user != null && article != null && article.User.Id != userId;
    }
}