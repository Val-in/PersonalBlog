using PersonalBlog.Core.Models;

namespace PersobalBlog.Core.Repositories;

public interface IUserRepository 
{
    User GetById(int id);
    void Add(User user);
}