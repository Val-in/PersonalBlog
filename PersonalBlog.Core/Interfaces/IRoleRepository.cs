using PersonalBlog.Core.Models;

namespace PersonalBlog.Core.Interfaces;

public interface IRoleRepository
{
    Role? GetById(int id);
    Role? GetByName(string name);
    IEnumerable<Role> GetAll();
}