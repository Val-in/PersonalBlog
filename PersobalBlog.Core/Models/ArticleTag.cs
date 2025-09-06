namespace PersonalBlog.Core.Models;

public class ArticleTag //таблица связей для сущностей «Статья» и её «Теги»
{
    public Guid ArticleId { get; set; }
    public Article Article { get; set; }

    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}