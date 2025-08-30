using PersobalBlog.Core.Repositories;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) => _context = context;

    public User GetById(Guid id) => _context.Users.Find(id); //возвращаем Core User, потом он будет передан в DTO, который содержит только данные, нужные Web/UI
    public void Add(User user) => _context.Users.Add(user);
}