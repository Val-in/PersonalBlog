using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Web.Controllers;

public class CommentController(AppCommentService commentService) : Controller
{
    [HttpPost]
    public IActionResult Create(Guid userId, Guid articleId, string text)
    {
        var success = commentService.AddComment(userId, articleId, text);
        if (!success) return BadRequest("Cannot create comment");

        return Ok("Comment created");
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        var comment = commentService.GetById(id);
        if (comment == null) return NotFound();

        return Ok(comment);
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var comments = commentService.GetAll();
        return Ok(comments);
    }

    [HttpPost("edit/{id:guid}")]
    public IActionResult Edit(Guid id, string newText)
    {
        var success = commentService.UpdateComment(id, newText);
        if (!success) return BadRequest("Cannot update comment");

        return Ok("Comment updated");
    }

    [HttpPost("delete/{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var success = commentService.DeleteComment(id);
        if (!success) return BadRequest("Cannot delete comment");

        return Ok("Comment deleted");
    }
}