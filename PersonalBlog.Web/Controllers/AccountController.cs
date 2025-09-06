using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.DTO;
using PersonalBlog.Application.Services;


namespace PersobalBlog.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller 
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto dto)
    {
        var user = _userService.ValidateUser(dto);
        if (user == null) return Unauthorized(new { message = "Неверный логин или пароль" });

        // Создаём клаймы
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserLogin.ToString()),
            new Claim(ClaimTypes.Name, user.UserLogin)
        };

        // Добавляем роли в клаймы
        foreach (var userRole in user.UserRoles)
        {
            if (userRole.Role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.RoleName));
            }
        }

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return Ok(new { message = "Вход выполнен успешно" });
    }
    
     // Logout доступно только администратору
        [HttpPost("logout")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Выход выполнен" });
        }
}