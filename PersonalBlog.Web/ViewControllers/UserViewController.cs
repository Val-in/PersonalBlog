using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Controllers.ViewControllers;

public class UserViewController : Controller
{
    public IActionResult Register() => View(); // список всех пользователей
    public IActionResult UserDetails() => View();
    public IActionResult UserEdit() => View();  // редактирование пользователя
    public IActionResult UserList() => View();
}