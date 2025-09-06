using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Controllers;

public class CommentController :  Controller
{
    private readonly AppCommentService _commentService;

    public CommentController(AppCommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public IActionResult Create(Guid userId, Guid articleId, string text)
    {
        var success = _commentService.AddComment(userId, articleId, text);
        if (!success) return BadRequest("Cannot create comment");

        return Ok("Comment created");
    }
    

    // Получить комментарий по id
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var comment = _commentService.GetById(id);
        if (comment == null) return NotFound();

        return Ok(comment);
    }

    // Получить все комментарии
    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var comments = _commentService.GetAll();
        return Ok(comments);
    }

    // Редактировать комментарий
    [HttpPost("edit/{id}")]
    public IActionResult Edit(Guid id, string newText)
    {
        var success = _commentService.UpdateComment(id, newText);
        if (!success) return BadRequest("Cannot update comment");

        return Ok("Comment updated");
    }

    // Удалить комментарий
    [HttpPost("delete/{id}")]
    public IActionResult Delete(Guid id)
    {
        var success = _commentService.DeleteComment(id);
        if (!success) return BadRequest("Cannot delete comment");

        return Ok("Comment deleted");
    }
}