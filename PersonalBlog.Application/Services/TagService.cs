using PersonalBlog.Application.DTO;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Application.Services;

public class TagService(ITagRepository tagRepository)
{
    public bool AddTag(TagDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.TagName)) return false;

        var tag = new Tag
        {
            TagName = dto.TagName,
            IsPersonal = dto.IsPersonal ?? false,
            CreatedAt = DateTime.UtcNow,
            UserId =  dto.UserId
        };

        return tagRepository.Add(tag);
    }

    public TagEditDto? GetById(Guid id)
    {
        var tag = tagRepository.GetById(id);
        if (tag == null) return null;

        return new TagEditDto
        {
            Name = tag.TagName
        };
    }

    public IEnumerable<TagDto> GetAll()
    {
        return tagRepository.GetAll()
            .Select(t => new TagDto { TagId = t.TagId, TagName = t.TagName })
            .ToList();
    }

    /// <summary>
    /// Можно редактировать только свой личный тег или публичный
    /// </summary>
    public bool UpdateTag(Guid id, Guid userId, string newName)
    {
        var tag = tagRepository.GetById(id);
        if (tag == null) return false;
        
        if (tag.UserId.HasValue && tag.UserId.Value != userId) return false;

        tag.TagName = newName;
        return tagRepository.Update(tag);
    }

    /// <summary>
    /// Удалить можно только свой личный тег или публичный
    /// </summary>
    public bool DeleteTag(Guid id, Guid userId)
    {
        var tag = tagRepository.GetById(id);
        if (tag == null) return false;
        
        if (tag.UserId.HasValue && tag.UserId.Value != userId) return false;

        return tagRepository.Delete(id);
    }
}
