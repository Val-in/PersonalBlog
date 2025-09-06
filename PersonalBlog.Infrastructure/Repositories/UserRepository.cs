using PersobalBlog.Core.Repositories;
using PersonalBlog.Core.Models;


namespace PersonalBlog.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) => _context = context;

    public User GetById(Guid id) => _context.Users.Find(id); //возвращаем Core User, потом он будет передан в DTO, который содержит только данные, нужные Web/UI
    public void Add(User user) {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
    
    public User? GetByLogin(string login) => _context.Users.FirstOrDefault(u => u.UserLogin == login);
    
    public IEnumerable<User> GetAll() => _context.Users.ToList();
    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}