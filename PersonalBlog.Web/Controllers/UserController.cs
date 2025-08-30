using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Controllers;

public class UserController : Controller //Контроллеры вызывают Application сервисы, а не DbContext напрямую
{
    private readonly ILogger<UserController> _logger;
    private readonly UserService _service;

    public UserController(ILogger<UserController> logger, UserService service)
    {
        _logger = logger;
        _service = service;
    }
    
    /*регистрация (присваиваемая роль - пользователь),
     редактирование,
     удаление пользователя,
     получение пользователя по айди,
     получение всех пользователей*/
    
}