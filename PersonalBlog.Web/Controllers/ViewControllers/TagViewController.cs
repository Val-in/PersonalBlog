using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Controllers.ViewControllers;

public class TagViewController : Controller
{
    public IActionResult Create() => View(); // создание тега
    public IActionResult TagEdit() => View();   // редактирование тега
    public IActionResult TagList() {
        
        return View("~/Views/TagView/TagList.cshtml");
    }
    
}