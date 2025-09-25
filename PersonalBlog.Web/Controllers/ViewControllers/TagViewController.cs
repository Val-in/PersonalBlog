using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Controllers.ViewControllers;

public class TagViewController : Controller
{
    public IActionResult Create() => View(); // создание тега
    public IActionResult Index() => View(); // список тегов
    public IActionResult TagDetails() => View();
    public IActionResult TagEdit() => View();   // редактирование тега
    public IActionResult TagList() => View(); // список тегов
    
}