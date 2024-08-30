using ecommerce_dash_api.Data;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly EcommerceContext _context;

    public UserRepository(EcommerceContext context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(User user)
    {
        _context.Users.Add(user);
    }

    public async Task DeleteUserRolesAsync(int userId)
    {
        var userRoles = await _context.UserRoles
           .Where(ur => ur.UserId == userId)
           .ToListAsync();

        _context.UserRoles.RemoveRange(userRoles);
    }

    public async Task CreateUserRolesAsync(int userId, List<int> roleIds)
    {
        var userRoles = roleIds.Select(roleId => new UserRole
        {
            UserId = userId,
            RoleId = roleId
        }).ToList();

        await _context.UserRoles.AddRangeAsync(userRoles);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<(User user, List<string> roles, List<string> permissions)> GetUserByUsernameAsync(string username)
    {
        var userWithRolesAndPermissions = await _context.Users
            .Where(u => u.Username == username)
            .Select(u => new
            {
                User = u,
                Roles = u.UserRoles.Select(ur => ur.Role.RoleName).ToList(),
                Permissions = u.UserRoles.SelectMany(ur => ur.Role.RolePermissions.Select(rp => rp.Permission.PermissionName)).ToList()
            })
            .FirstOrDefaultAsync();


        return (userWithRolesAndPermissions?.User, userWithRolesAndPermissions?.Roles, userWithRolesAndPermissions?.Permissions);
    }

    public async Task<bool> CreateRoleAsync(Role role, List<int> permissionIds)
    {

        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();

        foreach (var permissionId in permissionIds)
        {
            var rolePermission = new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permissionId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.RolePermissions.AddAsync(rolePermission);
        }

        return true;

    }

    public async Task DeleteRolePermissionsAsync(int roleId)
    {
        var rolePermissions = await _context.RolePermissions
           .Where(rp => rp.RoleId == roleId)
           .ToListAsync();

        _context.RolePermissions.RemoveRange(rolePermissions);
    }

    public async Task<bool> CreateRolePermissionsAsync(int roleId, List<int> permissionIds)
    {
        foreach (var permissionId in permissionIds)
        {
            var rolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.RolePermissions.AddAsync(rolePermission);
        }

        return true;
    }

    public async Task DeleteRoleAsync(int roleId)
    {
        var role = await _context.Roles
       .Where(r => r.Id == roleId)
       .FirstOrDefaultAsync();

        _context.Roles.Remove(role);
    }

    public async Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync()
    {
        var result = await _context.Roles
        .Select(r => new RoleQRY
        {
            Id = r.Id,
            RoleName = r.RoleName,
            Permissions = r.RolePermissions.Select(rp => new PermissionQRY
            {
                Id = rp.Permission.Id,
                PermissionName = rp.Permission.PermissionName
            }).ToList()
        })
        .ToListAsync();

        return result;

    }

    public async Task<List<PermissionQRY>> GetAllPermissionsAsync()
    {
        var result = await _context.Permissions
       .Select(p => new PermissionQRY
       {
           Id = p.Id,
           PermissionName = p.PermissionName,
       })
       .ToListAsync();

        return result;
    }

    public async Task<List<UserWithRolesAndPermissionsQRY>> GetAllUsersWithRolesAndPermissions()
    {
        var result = await _context.Users.Select(u => new UserWithRolesAndPermissionsQRY
        {
            FirstName = u.FirstName,
            LastName = u.LastName,
            Age = u.Age,
            Phone = u.Phone,
            Address = u.Address,
            Username = u.Username,
            Roles = u.UserRoles.Select(ur => new RoleQRY
            {
                Id = ur.Id,
                RoleName = ur.Role.RoleName
            }).ToList(),

            Permissions = u.UserRoles.SelectMany(ur => ur.Role.RolePermissions.Select(rp => new PermissionQRY{
                Id = rp.Permission.Id,
                PermissionName = rp.Permission.PermissionName
            })).ToList(),

        }).ToListAsync();


        var result2 = await _context.Users.Select(u => new UserWithRolesAndPermissionsQRY
        {
            FirstName = u.FirstName,
            LastName = u.LastName,
            Age = u.Age,
            Phone = u.Phone,
            Address = u.Address,
            Username = u.Username,
            Roles = u.UserRoles.Select(ur => new RoleQRY
            {
                Id = ur.Id,
                RoleName = ur.Role.RoleName
            }).ToList(),

            Permissions = u.UserRoles.SelectMany(ur => ur.Role.RolePermissions.Select(rp => new PermissionQRY
            {
                Id = rp.Permission.Id,
                PermissionName = rp.Permission.PermissionName
            })).ToList(),

        }).ToListAsync();

        return result;
    }
}



