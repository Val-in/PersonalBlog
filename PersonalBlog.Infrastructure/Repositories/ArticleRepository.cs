using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Infrastructure.Repositories;

public class ArticleRepository(AppDbContext context) : IArticleRepository
{
    public void Add(Article article)
    {
        context.Articles.Add(article);
        context.SaveChanges();
    }

    public Article? GetById(Guid id)
    {
        return context.Articles
            .Include(a => a.ArticleTags)
            .ThenInclude(at => at.Tag)
            .Include(a => a.User)
            .FirstOrDefault(a => a.ArticleId == id);
    }


    public IEnumerable<Article> GetByAuthor(Guid authorId)
    {
        return context.Articles.Where(a => a.AuthorId == authorId).ToList();
    }

    public void Update(Article article)
    {
        context.SaveChanges();
    }

    public void Delete(Article article)
    {
        context.Articles.Remove(article);
        context.SaveChanges();
    }

    public List<Article> GetAll()
    {
        return context.Articles
            .Include(a => a.ArticleTags)
            .ThenInclude(at => at.Tag)
            .Include(a => a.User) // если нужен автор
            .ToList();
    }
}