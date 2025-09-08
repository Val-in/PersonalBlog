using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;
using PersonalBlog.Application.DTO;

namespace PersonalBlog.Application.Services;

/// <summary>
/// Web Layer ->  Application -> IUserRepository (Core) -> UserRepository (Infrastructure) -> Database 
/// </summary>
public class
    UserService
    (IUserRepository repo, IRoleRepository roleRepository)
{
    public User CreateUser(UserDto dto, string? roleName = null)
    {
        var user = new User
        {
            UserLogin = dto.Login,
            UserNickName = dto.Nickname,
            Password = dto.Password,
            LoginDate = DateTime.UtcNow
        };

        repo.Add(user);

        // Назначаем роль
        string roleToAssign = roleName ?? "Пользователь"; 
        var role = roleRepository.GetByName(roleToAssign);
        if (role != null)
        {
            user.UserRoles.Add(new UserRoles
            {
                User = user,
                Role = role
            });
            repo.Update(user);
        }

        return user;
    }

    public User? ValidateUser(UserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Login) || string.IsNullOrWhiteSpace(dto.Password))
            return null;

        var user = repo.GetByLogin(dto.Login);
        return user.Password == dto.Password ? user : null;
    }

    public User? GetById(Guid id) => repo.GetById(id);

    public IEnumerable<User> GetAll() => repo.GetAll();

    public bool Update(Guid id, string dtoLogin, string dtoNickname)
    {
        var user = repo.GetById(id);

        user.UserLogin = dtoLogin;
        user.UserNickName = dtoNickname;
        repo.Update(user);
        return true;
    }

    public bool Delete(Guid id)
    {
        var user = repo.GetById(id);
        repo.Delete(user);
        return true;
    }
}