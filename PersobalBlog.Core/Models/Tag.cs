namespace PersonalBlog.Core.Models;

public class Tag
{
    public Guid TagId { get; set; }                 // уникальный идентификатор
    public string TagName { get; set; }            // название тега
    public Guid? UserId { get; set; }              // если тег личный, хранится владелец
    public DateTime CreatedAt { get; set; }        // дата создания
    public DateTime? UpdatedAt { get; set; }       // дата обновления (необязательная)

    // Навигационные свойства
    public User? User { get; set; }                // владелец тега
    public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>(); // связи с статьями
}