using PersobalBlog.Core.Repositories;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _context;

    public ArticleRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Article article)
    {
        _context.Articles.Add(article);
        _context.SaveChanges();
    }

    public Article? GetById(Guid id)
    {
        return _context.Articles.Find(id);
    }

    public IEnumerable<Article> GetByAuthor(Guid authorId)
    {
        return _context.Articles.Where(a => a.AuthorId == authorId).ToList();
    }

    public void Update(Article article)
    {
        _context.Articles.Update(article);
        _context.SaveChanges();
    }

    public void Delete(Article article)
    {
        _context.Articles.Remove(article);
        _context.SaveChanges();
    }
}