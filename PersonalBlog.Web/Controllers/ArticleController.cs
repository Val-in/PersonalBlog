using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Web.Controllers;

[Route("[controller]")]
public class ArticleController(ArticleService articleService) : Controller
{
    // Создание статьи
    [HttpPost("create")]
    public IActionResult Create(Guid authorId, string title, string content)
    {
        var success = articleService.AddArticle(authorId, title, content);
        if (!success) return BadRequest("Cannot create article");

        return Ok("Article created");
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        var article = articleService.GetById(id);
        if (article == null) return NotFound();

        return Ok(article);
    }

    [HttpGet("by-author/{authorId:guid}")]
    public IActionResult GetByAuthor(Guid authorId)
    {
        var articles = articleService.GetByAuthor(authorId);
        return Ok(articles);
    }
    
    [HttpGet("Article/details/{id}")]
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