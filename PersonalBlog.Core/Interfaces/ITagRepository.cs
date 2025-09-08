using PersonalBlog.Core.Models;

namespace PersonalBlog.Core.Interfaces;

public interface ITagRepository
{
    bool Add(Tag tag);
    Tag? GetById(Guid id);
    IEnumerable<Tag> GetAll();
    bool Update(Tag tag);
    bool Delete(Guid id);
}