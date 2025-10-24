using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Controllers.ViewControllers;

public class ArticleViewController : Controller
{
    public IActionResult ArticleCreate() => View();
    public IActionResult ArticleDetails() => View();
    public IActionResult ArticleEdit() => View();
    public IActionResult ArticleList() => View();
}