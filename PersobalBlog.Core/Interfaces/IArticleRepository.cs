using PersonalBlog.Core.Models;

namespace PersobalBlog.Core.Repositories;

public interface IArticleRepository
{
    void Add(Article article);
    Article? GetById(Guid id);
    IEnumerable<Article> GetByAuthor(Guid authorId);
    void Update(Article article);
    void Delete(Article article);
}