using PersonalBlog.Core.Models;

namespace PersobalBlog.Core.Repositories;

public interface IArticleRepository
{
    Article GetById(Guid id);
}