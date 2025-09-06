using PersobalBlog.Core.Repositories;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Application.Services;

public class ArticleService
{
    private readonly IArticleRepository _repo;

    public ArticleService(IArticleRepository repo)
    {
        _repo = repo;
    }

    public bool AddArticle(Guid authorId, string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            return false;

        var article = new Article //AddArticle не маппит DTO в Entity, а просто создаёт Entity из параметров метода.
        {
            AuthorId = authorId,
            Title = title,
            Content = content
        };

        _repo.Add(article);
        return true;
    }

    public Article? GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public IEnumerable<Article> GetByAuthor(Guid authorId)
    {
        return _repo.GetByAuthor(authorId);
    }

    public bool UpdateArticle(Guid id, string newTitle, string newContent)
    {
        var article = _repo.GetById(id);
        if (article == null) return false;

        article.Title = newTitle;
        article.Content = newContent;
        _repo.Update(article);
        return true;
    }

    public bool DeleteArticle(Guid id)
    {
        var article = _repo.GetById(id);
        if (article == null) return false;

        _repo.Delete(article);
        return true;
    }
}