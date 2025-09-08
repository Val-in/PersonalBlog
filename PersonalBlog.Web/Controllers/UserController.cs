using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.DTO;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Web.Controllers;

/// <summary>
/// [ApiController] Обозначает, что контроллер работает как API (JSON).
/// Автоматически проверяет валидность входных данных (ModelState. IsValid) и возвращает 400 BadRequest, если DTO некорректный.
/// Позволяет использовать атрибуты [FromBody], [FromQuery] и т.д.
/// Route Определяет базовый маршрут для всех действий. [controller] заменяется на имя класса без Controller.
///Пример: UserController → api/user.
/// </summary>
/// <param name="logger"></param>
/// <param name="service"></param>
[ApiController]
[Route("api/[controller]")]
public class UserController(ILogger<UserController> logger, UserService service) : Controller 
{ 
    /// <summary>
    /// [FromBody] Обозначает, что данные приходят в теле запроса (JSON).
    /// Если есть [ApiController], для POST/PUT запросов DTO по умолчанию берётся из тела.
    /// [FromBody] можно опустить, но многие ставят его для ясности кода.
    /// </summary>
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserDto dto, [FromQuery] string? role) 
    {
        try
        {
            var user = service.CreateUser(dto, role);
            return Ok(new { message = "Пользователь зарегистрирован", userId = user.Id });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при регистрации пользователя");
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var user = service.GetById(id);
        if (user == null) return NotFound(new { message = "Пользователь не найден" });
        return Ok(user);
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = service.GetAll();
        return Ok(users);
    }
    
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] UserDto dto)
    {
        try
        {
            var updated = dto is { Login: not null, Nickname: not null } && service.Update(id, dto.Login, dto.Nickname);
            if (!updated) return NotFound(new { message = "Пользователь не найден" });
            return Ok(new { message = "Пользователь обновлён" });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении пользователя");
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var deleted = service.Delete(id);
        if (!deleted) return NotFound(new { message = "Пользователь не найден" });
        return Ok(new { message = "Пользователь удалён" });
    }
}