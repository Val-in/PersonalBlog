using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Infrastructure.Repositories;

public class TagRepository(AppDbContext context) : ITagRepository
{
    public bool Add(Tag tag)
    {
        tag.TagName = tag.TagName!.Trim();
        context.Tags.Add(tag);
        context.SaveChanges();
        return true;
    }

    public Tag? GetById(Guid id)
    {
        return context.Tags.Find(id);
    }

    public IEnumerable<Tag> GetAll()
    {
        return context.Tags.ToList();
    }

    public bool Update(Tag tag)
    {
        var existing = context.Tags.Find(tag.TagId);
        if (existing == null) return false;

        existing.TagName = tag.TagName;
        context.Tags.Update(existing);
        context.SaveChanges();
        return true;
    }

    public bool Delete(Guid id)
    {
        var tag = context.Tags.Find(id);
        if (tag == null) return false;

        context.Tags.Remove(tag);
        context.SaveChanges();
        return true;
    }
}
