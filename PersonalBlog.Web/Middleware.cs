using LoggerNLog;

namespace PersonalBlog.Web;

public class Middleware
{
    private readonly RequestDelegate _next;

    public Middleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // передаем запрос дальше
        }
        catch (System.Exception ex)
        {
            Logging.LogError(ex, "Глобальная ошибка");
            
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync("<h1>Что-то пошло не так</h1>");
        }
    }
}