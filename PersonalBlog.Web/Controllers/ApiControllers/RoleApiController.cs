using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.DTO;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleApiController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleApiController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var roles = _roleService.GetAll();
        if (roles == null || !roles.Any())
            return NotFound();

        return Ok(roles); // возвращаем JSON
    }
    
    [HttpPost("edit/{id:int}")]
    public IActionResult Edit(int id, [FromBody] RoleDto dto)
    {
        if (id == null) return BadRequest("DTO is null");
        
        var role = _roleService.GetById(id); 
        if (role == null) return NotFound("Role not found");

        var success = _roleService.Update(role);
        return success ? Ok("Tag updated") : BadRequest("Cannot update tag");
    }
}