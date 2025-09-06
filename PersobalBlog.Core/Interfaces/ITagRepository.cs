using PersonalBlog.Core.Models;

namespace PersobalBlog.Core.Repositories;

public interface ITagRepository
{
    bool Add(Tag tag);
    Tag? GetById(Guid id);
    IEnumerable<Tag> GetAll();
    bool Update(Tag tag);
    bool Delete(Guid id);
}