using PersonalBlog.Application.DTO;
using PersonalBlog.Core.Interfaces;

namespace PersonalBlog.Application.Services;

public class RoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public List<RoleDto> GetAll()
    {
        return _roleRepository.GetAll()
            .Select(r => new RoleDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
                Description = r.Description ?? ""
            }).ToList();
    }

    public RoleDto GetById(int id)
    {
        var role = _roleRepository.GetById(id); // возвращает Role из БД
        if (role == null) return null!;

        return new RoleDto
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName ?? "",
            Description = role.Description ?? ""
        };
    }

    public bool Update(RoleDto dto)
    {
        var role = _roleRepository.GetById(dto.RoleId);
        if (role == null) return false;

        role.RoleName = dto.RoleName;
        role.Description = dto.Description;

        return _roleRepository.Update(role);
    }
}