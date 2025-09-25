using PersonalBlog.Application.DTO;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Application.Services;

public class ArticleService(IArticleRepository repo)
{
    /// <summary>
    /// AddArticle не маппит DTO в Entity, а просто создаёт Entity из параметров метода.
    /// </summary>
    public bool AddArticle(ArticleDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Content))
            return false;

        var article = new Article
        {
            AuthorId = dto.AuthorId,
            Title = dto.Title,
            Content = dto.Content
        };

        repo.Add(article);
        return true;
    }

    public ArticleDto? GetById(Guid id) //Сервис не должен возвращать Article?, а должен возвращать ArticleDto?
    {
        var article = repo.GetById(id);
        if (article == null) return null;

        return new ArticleDto
        {
            Id = article.ArticleId,
            Title = article.Title,
            Content = article.Content,
            AuthorId = article.AuthorId,
            Tags = article.ArticleTags.Select(t => new TagDto
            {
                TagId = t.TagId,
                TagName = t.Tag.TagName
            }).ToList()
            
        };
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