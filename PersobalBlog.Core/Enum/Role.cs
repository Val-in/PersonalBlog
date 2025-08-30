namespace PersobalBlog.Core.Enum;

public enum Role
{
    Admin = 1, //полные права
    Moderator = 2, //только статьи и комментарии
    User = 3 //только свои статьи, комментарии, теги
}