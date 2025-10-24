using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Controllers.ViewControllers;

public class AccountViewController : Controller
{
    public IActionResult Login() => View();
}