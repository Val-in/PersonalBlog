using LoggerNLog;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.DTO;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Api.ApiControllers;

/// <summary>
/// [ApiController] Обозначает, что контроллер работает как API (JSON).
/// Автоматически проверяет валидность входных данных (ModelState. IsValid) и возвращает 400 BadRequest, если DTO некорректный.
/// Позволяет использовать атрибуты [FromBody], [FromQuery] и т.д.
/// Route Определяет базовый маршрут для всех действий. [controller] заменяется на имя класса без Controller.
///Пример: UserApiController → api/user.
/// </summary>
/// <param name="logger"></param>
/// <param name="service"></param>
[ApiController]
[Route("[controller]")]
public class UserApiController(ILogger<UserApiController> logger, UserService service) : Controller 
{ 
    /// <summary>
    /// [FromBody] Обозначает, что данные приходят в теле запроса (JSON).
    /// Если есть [ApiController], для POST/PUT запросов DTO по умолчанию берётся из тела.
    /// [FromBody] можно опустить, но многие ставят его для ясности кода.
    /// </summary>
    [HttpPost("register")]
    public ActionResult<UserResponseDto> Register([FromBody] UserDto dto) 
    {
        try
        {
            Logging.LogUserAction($"Попытка регистрации пользователя: {dto.Nickname}");

            var user = service.CreateUser(dto);
            var response = new UserResponseDto
            {
                Message = "Пользователь зарегистрирован",
                Nickname = user.UserNickName
            };

            Logging.LogUserAction($"Пользователь зарегистрирован: {user.UserNickName}");
            return Ok(response);
        }
        catch (Exception ex)
        {
            Logging.LogError(ex, "Ошибка при регистрации пользователя");
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new Exception("Тестовая ошибка");
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)] //нужно для Swagger
    public ActionResult<UserResponseDto> GetById(Guid id)
    {
        var user = service.GetById(id);
        if (user == null) return NotFound(new { message = "Пользователь не найден" });
        return Ok(user);
    }
    
    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var users = service.GetAll()
            .Select(u => new UserResponseDto
            {
                Id = u.Id,
                Login = u.UserLogin!,
                Nickname = u.UserNickName!,
                Roles = new List<string?>()
            });
        return Ok(users);
    }
    
    [HttpPut("{id:guid}")]
    public ActionResult<UserResponseDto> Update(Guid id, [FromBody] UserUpdateDto dto)
    {
        try
        {
            var updated = service.Update(id, dto.Login, dto.Nickname, dto.Password, dto.RoleId);
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