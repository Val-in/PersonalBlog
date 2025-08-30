using PersobalBlog.Core.Repositories;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Application.Services;

public class AppCommentService //здесь добавляем логику из Domain Service из Core. Формируем API для WEB
{
    private readonly AppCommentService _appCommentService; //Domain Service
    private readonly ICommentRepository _commentRepository;

    public AppCommentService(AppCommentService appCommentService, ICommentRepository commentRepository)
    {
        _appCommentService = appCommentService;
        _commentRepository = commentRepository;
    }

    public bool AddComment(User userId, Article articleId, string text)
    {
        if (_appCommentService.CanUserComment(userId, articleId)) //ДОБАВИТЬ МЕТОД!
        {
            _commentRepository.Add(new Comment { UserId = userId, ArticleId = articleId, CommentText = text });
            return true;
        }
        return false;
    }
}