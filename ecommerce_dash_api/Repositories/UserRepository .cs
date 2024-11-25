using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using Microsoft.EntityFrameworkCore;
using System.Data;

public class UserRepository : IUserRepository
{
    private readonly EcommerceContext _context;

    public UserRepository(EcommerceContext context)
    {
        _context = context;
    }

    //+------------------------------------------------------------------+
    //| User                                            
    //+------------------------------------------------------------------+
    public async Task<User?> GetUserAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
    }
    public async Task<(User? user, List<string>? roles, List<string>? permissions)> GetUserByUsernameAsync(string username)
    {
        var userWithRolesAndPermissions = await _context.Users
            .Where(i => i.Username == username)
            .Select(i => new
            {
                User = i,
                Roles = i.UserRoleUsers.Select(ur => ur.Role.Name).ToList(),
                Permissions = i.UserRoleUsers.SelectMany(ur => ur.Role.RolePermissions.Select(rp => rp.Permission.Name)).ToList()
            })
            .FirstOrDefaultAsync();

        return (userWithRolesAndPermissions?.User, userWithRolesAndPermissions?.Roles, userWithRolesAndPermissions?.Permissions);
    }
    public async Task<List<UserWithRolesAndPermissionsQRY>> GetAllUsersWithRolesAndPermissions()
    {
        var result = await _context.Users
             .Where(u => u.Id != 1)
            .Select(u => new UserWithRolesAndPermissionsQRY
            {
                Id = u.Id,
                Username = u.Username,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Dob = u.Dob,
                Phone = u.Phone,
                Address = u.Address,
                UpdatedAt = u.UpdatedAt,
                UpdatedBy = u.UpdatedBy,
                FailedLoginAttempts = u.FailedLoginAttempts,
                IsActive = u.IsActive,


                Roles = u.UserRoleUsers.Select(ur => new RoleWithoutPermissionsQRY
                {
                    Id = ur.Role.Id,
                    RoleName = ur.Role.Name
                }).ToList(),

                Permissions = u.UserRoleUsers.SelectMany(ur => ur.Role.RolePermissions.Select(rp => new PermissionQRY
                {
                    Id = rp.Permission.Id,
                    PermissionName = rp.Permission.Name
                })).ToList(),

            }).ToListAsync();

        return result;
    }
    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }
    public async Task CreateUserAsync(User user)
    {
       await _context.Users.AddAsync(user);
    }
    public Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }
    public Task DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        return Task.CompletedTask;
    }
    public Task DeleteUserRolesAsync(List<UserRole> userRoles)
    {
        _context.UserRoles.RemoveRange(userRoles);
        return Task.CompletedTask;
    }


    //+------------------------------------------------------------------+
    //| Role                                            
    //+------------------------------------------------------------------+
    public async Task<Role?> GetRoleByIdAsync(int id)
    {
        return await _context.Roles.FirstOrDefaultAsync(i => i.Id == id);
    }
    public async Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync()
    {
        var result = await _context.Roles
        .Select(i => new RoleQRY
        {
            Id = i.Id,
            RoleName = i.Name,
            Permissions = i.RolePermissions.Select(rp => new PermissionQRY
            {
                Id = rp.Permission.Id,
                PermissionName = rp.Permission.Name
            }).ToList()
        })
        .ToListAsync();

        return result;
    }
    public Task UpdateRoleAsync(Role role)
    {
        _context.Roles.Update(role);
        return Task.CompletedTask;
    }
    public Task DeleteRolePermissionsAsync(List<RolePermission> rolePermissions)
    {
        _context.RolePermissions.RemoveRange(rolePermissions);
        return Task.CompletedTask;
    }
    public Task DeleteRoleAsync(Role role)
    {
        _context.Roles.Remove(role);
        return Task.CompletedTask;
    }


    //+------------------------------------------------------------------+
    //| Permission                                            
    //+------------------------------------------------------------------+
    public async Task<List<PermissionQRY>> GetAllPermissionsAsync()
    {
        var result = await _context.Permissions
       .Select(i => new PermissionQRY
       {
           Id = i.Id,
           PermissionName = i.Name,
       })
       .ToListAsync();

        return result;
    }
}



