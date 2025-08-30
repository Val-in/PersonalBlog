using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.Services;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Controllers;

public class CommentController
{
    /* редактирование,
    удаление комментария,
    получение комментария по айди,
    получение всех комментариев*/
    
    private readonly AppCommentService _commentAppService;

    public CommentController(AppCommentService commentAppService)
    {
        _commentAppService = commentAppService;
    }

    [HttpPost]
    public IActionResult Create(User userId, Article articleId, string text)
    {
        var success = _commentAppService.AddComment(userId, articleId, text);
        if (success) return RedirectToAction("Details", "Article", new { id = articleId });
        return BadRequest("Cannot add comment");
    }
}