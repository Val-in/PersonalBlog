using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.DTO;
using PersonalBlog.Application.Services;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Controllers;

/* [ApiController] Обозначает, что контроллер работает как API (JSON).
Автоматически проверяет валидность входных данных (ModelState.IsValid) и возвращает 400 BadRequest, если DTO некорректный.
Позволяет использовать атрибуты [FromBody], [FromQuery] и т.д.*/
[ApiController]
/* Route Определяет базовый маршрут для всех действий. [controller] заменяется на имя класса без Controller.
Пример: UserController → api/user.*/
[Route("api/[controller]")]
public class UserController : Controller //Контроллеры вызывают Application сервисы, а не DbContext напрямую
{
    private readonly ILogger<UserController> _logger; //ГДЕ ИСПОЛЬЗУЕМ???
    private readonly UserService _service;

    public UserController(ILogger<UserController> logger, UserService service)
    {
        _logger = logger;
        _service = service;
    }
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserDto dto) //[FromBody] Обозначает, что данные приходят в теле запроса (JSON).
                                                            //Если есть [ApiController], для POST/PUT запросов DTO по умолчанию берётся из тела.
                                                            //→ [FromBody] можно опустить, но многие ставят его для ясности кода.
    {
        try
        {
            var user = _service.CreateUser(dto);
            return Ok(new { message = "Пользователь зарегистрирован", userId = user.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при регистрации пользователя");
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id) => Ok(new { id, name = "Test" });
    // {
    //     var user = _service.GetById(id);
    //     if (user == null) return NotFound(new { message = "Пользователь не найден" });
    //     return Ok(user);
    // }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _service.GetAll();
        return Ok(users);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] UserDto dto)
    {
        try
        {
            var updated = _service.Update(id, dto.Login, dto.Nickname);
            if (!updated) return NotFound(new { message = "Пользователь не найден" });
            return Ok(new { message = "Пользователь обновлён" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении пользователя");
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var deleted = _service.Delete(id);
        if (!deleted) return NotFound(new { message = "Пользователь не найден" });
        return Ok(new { message = "Пользователь удалён" });
    }
}