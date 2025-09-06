using PersobalBlog.Core.Repositories;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly AppDbContext _context;

    public TagRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool Add(Tag tag)
    {
        tag.TagName = tag.TagName.Trim();
        _context.Tags.Add(tag);
        _context.SaveChanges();
        return true;
    }

    public Tag? GetById(Guid id)
    {
        return _context.Tags.Find(id);
    }

    public IEnumerable<Tag> GetAll()
    {
        return _context.Tags.ToList();
    }

    public bool Update(Tag tag)
    {
        var existing = _context.Tags.Find(tag.TagId);
        if (existing == null) return false;

        existing.TagName = tag.TagName;
        _context.Tags.Update(existing);
        _context.SaveChanges();
        return true;
    }

    public bool Delete(Guid id)
    {
        var tag = _context.Tags.Find(id);
        if (tag == null) return false;

        _context.Tags.Remove(tag);
        _context.SaveChanges();
        return true;
    }
}
