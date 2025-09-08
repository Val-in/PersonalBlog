namespace PersonalBlog.Core.Models;

/// <summary>
/// Таблица связей для сущностей «Статья» и её «Теги»
/// </summary>
public class ArticleTag
{
    public Guid ArticleId { get; set; }
    public Article? Article { get; set; }

    public Guid TagId { get; set; }
    public Tag? Tag { get; set; }
}