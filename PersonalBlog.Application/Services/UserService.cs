using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.Models;
using PersonalBlog.Application.DTO;

namespace PersonalBlog.Application.Services;

/// <summary>
/// Web Layer ->  Application -> IUserRepository (Core) -> UserRepository (Infrastructure) -> Database 
/// </summary>
public class
    UserService
    (IUserRepository userRepository, IRoleRepository roleRepository)
{
    public User CreateUser(UserDto dto)
    {
        var user = new User
        {
            UserLogin = dto.Login,
            UserNickName = dto.Nickname,
            Email = dto.Email,
            Password = dto.Password,
            LoginDate = DateTime.UtcNow
        };
        
        // Назначаем роль
        string roleToAssign = "User"; 
        var role = roleRepository.GetByName(roleToAssign);
        user.UserRoles.Add(new UserRoles { User = user, Role = role });
        
        userRepository.Add(user);

        return user;
    }

    public User? ValidateUser(LoginDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Login) || string.IsNullOrWhiteSpace(dto.Password))
            return null;

        var user = userRepository.GetByLogin(dto.Login);
        return user.Password == dto.Password ? user : null;
    }

    public UserResponseDto? GetById(Guid id) //возвращаем дто для контроллера
    {
        var user = userRepository.GetById(id);

        return new UserResponseDto
        {
            Id = user.Id,
            Login = user.UserLogin,
            Nickname = user.UserNickName,
            Roles = user.UserRoles.Select(ur => ur.Role?.RoleName).ToList()
        };
    }

    public IEnumerable<User> GetAll() => userRepository.GetAll();

    public bool Update(Guid id, string login, string nickname, string? password = null, int? roleId = null)
    {
        var user = userRepository.GetById(id);

        user.UserLogin = login;
        user.UserNickName = nickname;
        if (!string.IsNullOrEmpty(password))
        {
            user.Password = password;
        }

        if (roleId.HasValue)
        {
            user.UserRoles.Clear();
            
            user.UserRoles.Add(new UserRoles
            {
                UserId = user.Id,
                RoleId = roleId.Value
            });
        }

        userRepository.Update(user);
        return true;
    }


    public bool Delete(Guid id)
    {
        var user = userRepository.GetById(id);
        userRepository.Delete(user);
        return true;
    }
}