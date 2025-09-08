using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Infrastructure.Repositories;

public class RoleRepository(AppDbContext context) : IRoleRepository
{
    public Role? GetById(int id) => context.Roles.Find(id);

    public Role? GetByName(string name) => context.Roles.FirstOrDefault(r => r.RoleName == name);

    public IEnumerable<Role> GetAll() => context.Roles.ToList();
}