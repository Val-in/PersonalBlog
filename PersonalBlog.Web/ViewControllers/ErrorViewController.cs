using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Controllers.ViewControllers;

public class ErrorViewController : Controller
{
    public IActionResult Error() => View();
    public IActionResult Forbidden()
    {
        return View("~/Views/ErrorView/Forbidden.cshtml");
    }
    public IActionResult NotFoundView() => View();
}