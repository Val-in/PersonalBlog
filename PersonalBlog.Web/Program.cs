using PersobalBlog.Core.Repositories;
using PersonalBlog.Application.Services;
using PersonalBlog.Infrastructure;
using PersonalBlog.Infrastructure.Repositories;

namespace PersonalBlog.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddScoped<IUserRepository, UserRepository>(); //Scoped – один экземпляр на запрос (HTTP Request)
        builder.Services.AddScoped<UserService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
           
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}