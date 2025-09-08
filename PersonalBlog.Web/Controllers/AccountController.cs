using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Application.DTO;
using PersonalBlog.Application.Services;

namespace PersonalBlog.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(UserService userService) : Controller
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto dto)
    {
        var user = userService.ValidateUser(dto);
        if (user == null) return Unauthorized(new { message = "Неверный логин или пароль" });

        if (user.UserLogin != null)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserLogin),
                new(ClaimTypes.Name, user.UserLogin)
            };

            if (user.UserRoles != null)
                foreach (var userRole in user.UserRoles)
                {
                    if (userRole.Role != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role,
                            userRole.Role.RoleName ?? throw new InvalidOperationException()));
                    }
                }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        return Ok(new { message = "Вход выполнен успешно" });
    }
    
     /// <summary>
     /// Logout доступно только администратору
     /// </summary>
     [HttpPost("logout")] 
     [Authorize(Roles = "Admin")] 
     public async Task<IActionResult> Logout()
     {
         await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
         return Ok(new { message = "Выход выполнен" });
     }
}