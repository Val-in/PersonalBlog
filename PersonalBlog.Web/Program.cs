using Microsoft.EntityFrameworkCore;
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

// Add services
        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<AppCommentService>();
        builder.Services.AddScoped<ITagRepository, TagRepository>();
        builder.Services.AddScoped<TagService>();
        builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
        builder.Services.AddScoped<ArticleService>();

        var app = builder.Build();

        //app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}