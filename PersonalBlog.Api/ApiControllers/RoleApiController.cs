using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.DTO;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Api.ApiControllers;

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
        try
        {
            var roles = _roleService.GetAll();
            if (roles == null || !roles.Any())
                return NotFound();

            return Ok(roles);
        }
        catch (Exception ex)
        {
            // Логирование
            Console.WriteLine(ex);
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("edit/{id:int}")]
    public IActionResult Edit(int id, [FromBody] RoleDto dto)
    {
        var role = _roleService.GetById(id);

        var success = _roleService.Update(role);
        return success ? Ok("Tag updated") : BadRequest("Cannot update tag");
    }
}