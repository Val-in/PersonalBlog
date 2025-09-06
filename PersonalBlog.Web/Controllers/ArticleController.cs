using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Controllers;

[Route("[controller]")]
public class ArticleController : Controller
{
    private readonly ArticleService _articleService;

    public ArticleController(ArticleService articleService)
    {
        _articleService = articleService;
    }

    // Создание статьи
    [HttpPost("create")]
    public IActionResult Create(Guid authorId, string title, string content)
    {
        var success = _articleService.AddArticle(authorId, title, content);
        if (!success) return BadRequest("Cannot create article");

        return Ok("Article created");
    }

    // Получение статьи по id
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var article = _articleService.GetById(id);
        if (article == null) return NotFound();

        return Ok(article);
    }

    // Получение всех статей по автору
    [HttpGet("by-author/{authorId}")]
    public IActionResult GetByAuthor(Guid authorId)
    {
        var articles = _articleService.GetByAuthor(authorId);
        return Ok(articles);
    }
    
    [HttpGet("{id}")]
    public IActionResult Details(Guid id)
    {
        var articleDto = _articleService.GetById(id); 
        if (articleDto == null) return NotFound();

        return Ok(articleDto); // возвращаем JSON
    }

    // Редактирование статьи
    [HttpPost("edit/{id}")]
    public IActionResult Edit(Guid id, string newTitle, string newContent)
    {
        var success = _articleService.UpdateArticle(id, newTitle, newContent);
        if (!success) return BadRequest("Cannot update article");

        return Ok("Article updated");
    }

    // Удаление статьи
    [HttpPost("delete/{id}")]
    public IActionResult Delete(Guid id)
    {
        var success = _articleService.DeleteArticle(id);
        if (!success) return BadRequest("Cannot delete article");

        return Ok("Article deleted");
    }
}