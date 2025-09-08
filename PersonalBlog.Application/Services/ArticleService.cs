using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Application.Services;

public class ArticleService(IArticleRepository repo)
{
    /// <summary>
    /// AddArticle не маппит DTO в Entity, а просто создаёт Entity из параметров метода.
    /// </summary>
    public bool AddArticle(Guid authorId, string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            return false;

        var article = new Article
        {
            AuthorId = authorId,
            Title = title,
            Content = content
        };

        repo.Add(article);
        return true;
    }

    public Article? GetById(Guid id)
    {
        return repo.GetById(id);
    }

    public IEnumerable<Article> GetByAuthor(Guid authorId)
    {
        return repo.GetByAuthor(authorId);
    }

    public bool UpdateArticle(Guid id, string newTitle, string newContent)
    {
        var article = repo.GetById(id);
        if (article == null) return false;

        article.Title = newTitle;
        article.Content = newContent;
        repo.Update(article);
        return true;
    }

    public bool DeleteArticle(Guid id)
    {
        var article = repo.GetById(id);
        if (article == null) return false;

        repo.Delete(article);
        return true;
    }
}