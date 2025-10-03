using System.Security.Claims;
using PersonalBlog.Application.DTO;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Application.Services;

public class ArticleService(IArticleRepository articleRepository, IUserRepository userRepository)
{
    public bool AddArticle(ArticleCreateDto dto, ClaimsPrincipal user)
    {
        if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Content))
            return false; 
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return false;

        var article = new Article
        {
            Title = dto.Title,
            Content = dto.Content,
            AuthorId = Guid.Parse(userId), // привязываем текущего юзера
            ArticleTags = dto.Tags
                .Select(t => new ArticleTag
                {
                    TagId = t
                })
                .ToList()
        };

        articleRepository.Add(article);
        return true;
    }

    public ArticleDto? GetById(Guid id) 
    {
        var article = articleRepository.GetById(id);
        if (article == null) return null;

        return new ArticleDto
        {
            Title = article.Title,
            Content = article.Content,
            AuthorName = article.User?.UserNickName ?? "Неизвестно",
            Tags = article.ArticleTags.Select(t => new TagDto
            {
                TagId = t.TagId,
                TagName = t.Tag.TagName
            }).ToList()
            
        };
    }

    public IEnumerable<Article> GetByAuthor(Guid authorId) //поменять на DTO
    {
        return articleRepository.GetByAuthor(authorId);
    }

    public bool UpdateArticle(Guid id, string newTitle, string newContent)
    {
        var article = articleRepository.GetById(id);
        if (article == null) return false;

        article.Title = newTitle;
        article.Content = newContent;
        articleRepository.Update(article);
        return true;
    }

    public bool DeleteArticle(Guid id)
    {
        var article = articleRepository.GetById(id);
        if (article == null) return false;

        articleRepository.Delete(article);
        return true;
    }

    public List<ArticleDto> GetAll()
    {
        var articles = articleRepository.GetAll();
        return articles.Select(article => new ArticleDto
        {
            Title = article.Title,
            Content = article.Content,
            Tags = article.ArticleTags.Select(t => new TagDto
            {
                TagId = t.TagId,
                TagName = t.Tag!.TagName
            }).ToList()
        }).ToList();
    }
}