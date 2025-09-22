using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(); // ищет Views/Home/Index.cshtml
    }
}