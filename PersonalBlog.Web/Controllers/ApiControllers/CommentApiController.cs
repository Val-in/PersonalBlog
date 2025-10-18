using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.DTO;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentApiController(AppCommentService commentService) : Controller
{
    [HttpPost("create")]
    public IActionResult Create([FromBody] CommentDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);
        var success = commentService.AddComment(userId, dto.ArticleId, dto.Text);
        if (!success) return BadRequest("Cannot create comment");

        return Ok("Comment created");
    }
    
    [HttpGet("{id:guid}")] //возвращает для редактирования статьи
    public IActionResult Get(Guid id)
    {
        var comment = commentService.GetById(id);
        if (comment == null) return NotFound();

        return Ok(comment);
    }
    
    [HttpGet("byarticle/{articleId}")] //возвращает комментарии статьи
    public IActionResult GetByArticle(Guid articleId)
    {
        var comments = commentService.GetByArticleId(articleId);
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