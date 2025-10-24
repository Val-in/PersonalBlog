using LoggerNLog;

namespace PersonalBlog.Api;

public class Middleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context); // передаем запрос дальше
        }
        catch (Exception ex)
        {
            Logging.LogError(ex, "Глобальная ошибка");
            
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync("<h1>Что-то пошло не так</h1>");
        }
    }
}