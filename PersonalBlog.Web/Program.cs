using Microsoft.EntityFrameworkCore;
using PersonalBlog.Application.Services;
using PersonalBlog.Core.DomainServices;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Infrastructure;
using PersonalBlog.Infrastructure.Repositories;

namespace PersonalBlog.Web;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        
        var dbPath = builder.Configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine("EF Core будет использовать SQLite файл: " + Path.GetFullPath(dbPath));
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<AppCommentService>();
        builder.Services.AddScoped<ITagRepository, TagRepository>();
        builder.Services.AddScoped<TagService>();
        builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
        builder.Services.AddScoped<ArticleService>();
        builder.Services.AddScoped<CommentService>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseDeveloperExceptionPage();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

    }
}