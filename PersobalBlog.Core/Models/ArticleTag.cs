namespace PersonalBlog.Core.Models;

public class ArticleTag //таблица связей для сущностей «Статья» и её «Теги»
{
    public Article Article { get; set; }
    public Tag Tag { get; set; }
}