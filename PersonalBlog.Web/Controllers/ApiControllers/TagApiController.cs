using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.DTO;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TagApiController(TagService tagService) : Controller
{
    [HttpPost("create")]
    public IActionResult Create([FromBody] TagDto dto)
    {
        var success = tagService.AddTag(dto);
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
