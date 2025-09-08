using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Web.Controllers;

[Route("[controller]")]
public class TagController(TagService tagService) : Controller
{
    [HttpPost("create")]
    public IActionResult Create(Guid userId, string name, bool isPersonal = false)
    {
        var success = tagService.AddTag(userId, name, isPersonal);
        if (!success) return BadRequest("Cannot create tag");

        return Ok("Tag created");
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        var tag = tagService.GetById(id);
        if (tag == null) return NotFound();

        return Ok(tag);
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var tags = tagService.GetAll();
        return Ok(tags);
    }

    [HttpPost("edit/{id:guid}")]
    public IActionResult Edit(Guid id, Guid userId, string newName)
    {
        var success = tagService.UpdateTag(id, userId, newName);
        if (!success) return BadRequest("Cannot update tag");

        return Ok("Tag updated");
    }

    [HttpPost("delete/{id:guid}")]
    public IActionResult Delete(Guid id, Guid userId)
    {
        var success = tagService.DeleteTag(id, userId);
        if (!success) return BadRequest("Cannot delete tag");

        return Ok("Tag deleted");
    }
}
