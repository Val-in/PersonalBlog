using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Controllers;

[Route("[controller]")]
public class TagController : Controller
{
    private readonly TagService _tagService;

    public TagController(TagService tagService)
    {
        _tagService = tagService;
    }

    // Создание тега (глобального или личного)
    [HttpPost("create")]
    public IActionResult Create(Guid userId, string name, bool isPersonal = false)
    {
        var success = _tagService.AddTag(userId, name, isPersonal);
        if (!success) return BadRequest("Cannot create tag");

        return Ok("Tag created");
    }

    // Получение тега по id
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var tag = _tagService.GetById(id);
        if (tag == null) return NotFound();

        return Ok(tag);
    }

    // Получение всех тегов
    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var tags = _tagService.GetAll();
        return Ok(tags);
    }

    // Редактирование тега
    [HttpPost("edit/{id}")]
    public IActionResult Edit(Guid id, Guid userId, string newName)
    {
        var success = _tagService.UpdateTag(id, userId, newName);
        if (!success) return BadRequest("Cannot update tag");

        return Ok("Tag updated");
    }

    // Удаление тега (только если он принадлежит пользователю)
    [HttpPost("delete/{id}")]
    public IActionResult Delete(Guid id, Guid userId)
    {
        var success = _tagService.DeleteTag(id, userId);
        if (!success) return BadRequest("Cannot delete tag");

        return Ok("Tag deleted");
    }
}
