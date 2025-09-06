using PersobalBlog.Core.Repositories;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Application.Services;

public class TagService
{
    public readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public bool AddTag(Guid userId, string name, bool isPersonal)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;

        var tag = new Tag
        {
            TagId = Guid.NewGuid(),
            TagName = name,
            UserId = isPersonal ? userId : null, // если личный, сохраняем владельца
            CreatedAt = DateTime.UtcNow
        };

        return _tagRepository.Add(tag);
    }

    public Tag? GetById(Guid id)
    {
        return _tagRepository.GetById(id);
    }

    public IEnumerable<Tag> GetAll()
    {
        return _tagRepository.GetAll();
    }

    public bool UpdateTag(Guid id, Guid userId, string newName)
    {
        var tag = _tagRepository.GetById(id);
        if (tag == null) return false;

        // можно редактировать только свой личный тег или публичный
        if (tag.UserId.HasValue && tag.UserId.Value != userId) return false;

        tag.TagName = newName;
        return _tagRepository.Update(tag);
    }

    public bool DeleteTag(Guid id, Guid userId)
    {
        var tag = _tagRepository.GetById(id);
        if (tag == null) return false;

        // удалить можно только свой личный тег или публичный
        if (tag.UserId.HasValue && tag.UserId.Value != userId) return false;

        return _tagRepository.Delete(id);
    }
}
