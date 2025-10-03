using System.Security.Claims;
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
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();

        dto.UserId = Guid.Parse(userIdClaim.Value);
        
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
    public IActionResult Edit(Guid id, [FromBody] TagEditDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);
        var success = tagService.UpdateTag(id, userId, dto.Name);

        return success ? Ok("Tag updated") : BadRequest("Cannot update tag");
    }

    [HttpPost("delete/{id:guid}")]
    public IActionResult Delete(Guid id, Guid userId)
    {
        var success = tagService.DeleteTag(id, userId);
        if (!success) return BadRequest("Cannot delete tag");

        return Ok("Tag deleted");
    }
}
