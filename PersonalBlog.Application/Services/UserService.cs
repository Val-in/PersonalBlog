using PersobalBlog.Core.Repositories;
using PersonalBlog.Core.Models;

namespace PersonalBlog.Application.Services;

public class UserService //Web Layer ->  Application -> IUserRepository (Core) -> UserRepository (Infrastructure) -> Database 
{
    //тут бизнес логика
    private readonly IUserRepository _repo;
    public UserService(IUserRepository repo) => _repo = repo;

    public void CreateUser(string name)
    {
        var user = new User { UserNickName = name };
        _repo.Add(user);
    }
}