using PersobalBlog.Core.Repositories;
using PersonalBlog.Application.DTO;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Application.Services;

public class UserService //Web Layer ->  Application -> IUserRepository (Core) -> UserRepository (Infrastructure) -> Database 
{
    //тут бизнес логика
    private readonly IUserRepository _repo;
    public UserService(IUserRepository repo) => _repo = repo;

    public User CreateUser(UserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserLogin = dto.Login,
            UserNickName = dto.Nickname,
            Password = dto.Password,
            LoginDate = DateTime.UtcNow
        };

        _repo.Add(user);
        return user;
    }

    public User? ValidateUser(UserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Login) || string.IsNullOrWhiteSpace(dto.Password))
            return null;

        var user = _repo.GetByLogin(dto.Login);
        return user != null && user.Password == dto.Password ? user : null;
    }

    public User? GetById(Guid id) => _repo.GetById(id);

    public IEnumerable<User> GetAll() => _repo.GetAll();

    public bool Update(Guid id, string dtoLogin, string dtoNickname)
    {
        var user = _repo.GetById(id);
        if (user == null) return false;

        user.UserLogin = dtoLogin;
        user.UserNickName = dtoNickname;
        _repo.Update(user);
        return true;
    }

    public bool Delete(Guid id)
    {
        var user = _repo.GetById(id);
        if (user == null) return false;

        _repo.Delete(user);
        return true;
    }
}