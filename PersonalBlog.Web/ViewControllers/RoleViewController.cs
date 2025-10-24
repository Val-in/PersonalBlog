using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Controllers.ViewControllers;

public class RoleViewController : Controller
{
    public IActionResult RoleCreate() => View();
    public IActionResult RoleEdit() => View();
    public IActionResult RoleList() => View();
}