using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;


namespace PersonalBlog.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    /// <summary>
    /// Возвращаем Core User, потом он будет передан в DTO, который содержит только данные, нужные Web/UI
    /// </summary>
    public User GetById(Guid id) => context.Users.Find(id) ?? throw new InvalidOperationException();
    public void Add(User user) {
        context.Users.Add(user);
        context.SaveChanges();
    }
    
    public User GetByLogin(string login) => context.Users
        .Include(u => u.UserRoles)           // подгружаем связи UserRoles
        .ThenInclude(ur => ur.Role)      // подгружаем Role внутри UserRoles
        .FirstOrDefault(u => u.UserLogin == login)!;
    
    public IEnumerable<User> GetAll() => context.Users.ToList();
    public void Update(User user)
    {
        context.Users.Update(user);
        context.SaveChanges();
    }

    public void Delete(User user)
    {
        context.Users.Remove(user);
        context.SaveChanges();
    }
}