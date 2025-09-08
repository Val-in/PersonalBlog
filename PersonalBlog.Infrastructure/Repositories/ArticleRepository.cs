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
        return context.Articles.Find(id);
    }

    public IEnumerable<Article> GetByAuthor(Guid authorId)
    {
        return context.Articles.Where(a => a.AuthorId == authorId).ToList();
    }

    public void Update(Article article)
    {
        context.Articles.Update(article);
        context.SaveChanges();
    }

    public void Delete(Article article)
    {
        context.Articles.Remove(article);
        context.SaveChanges();
    }
}