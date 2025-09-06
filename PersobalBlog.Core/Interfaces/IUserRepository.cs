using PersonalBlog.Core.Models;

namespace PersobalBlog.Core.Repositories;

public interface IUserRepository //для работы UserRepository в Infrastructure
{
    User GetById(Guid id);
    void Add(User user);
    
    User GetByLogin(string login);
    IEnumerable<User> GetAll();
    void Update(User user);
    void Delete(User user);
}