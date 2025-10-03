using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.DTO;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleApiController(ArticleService articleService) : Controller
{
    // Создание статьи
    [HttpPost("create")]
    public IActionResult Create([FromBody] ArticleCreateDto dto)
    {
        var success = articleService.AddArticle(dto, User);
        if (!success) return BadRequest("Cannot create article");

        return Ok("Article created");
    }

    [HttpGet("all")]
    public IActionResult Get()
    {
        var articles = articleService.GetAll();
        if (articles == null) return NotFound();

        return Ok(articles);
    }

    [HttpGet("by-author/{authorId:guid}")]
    public IActionResult GetByAuthor(Guid authorId)
    {
        var articles = articleService.GetByAuthor(authorId);
        return Ok(articles);
    }
    
    [HttpGet("details/{id}")]
    public IActionResult Details(Guid id)
    {
        var articleDto = articleService.GetById(id); 
        if (articleDto == null) return NotFound();

        return Ok(articleDto); // возвращаем JSON
    }

    [HttpPost("edit/{id:guid}")]
    public IActionResult Edit(Guid id, string newTitle, string newContent)
    {
        var success = articleService.UpdateArticle(id, newTitle, newContent);
        if (!success) return BadRequest("Cannot update article");

        return Ok("Article updated");
    }

    [HttpPost("delete/{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var success = articleService.DeleteArticle(id);
        if (!success) return BadRequest("Cannot delete article");

        return Ok("Article deleted");
    }
}