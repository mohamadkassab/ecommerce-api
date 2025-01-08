using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Areas.Dashboard.QRYS;
using ecommerce_dash_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ecommerce_dash_api.Areas.Dashboard.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EcommerceContext _context;

        public UserRepository(EcommerceContext context)
        {
            _context = context;
        }

        //+------------------------------------------------------------------+
        //| Permission                                            
        //+------------------------------------------------------------------+
        public async Task<List<PermissionQRY>> GetAllPermissionsAsync()
        {
            var result = await _context.Permissions
            .AsNoTracking()
           .Select(i => new PermissionQRY
           {
               Id = i.Id,
               Name = i.Name,
           })
           .ToListAsync();

            return result;
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
            .AsNoTracking()
            .Select(i => new RoleQRY
            {
                Id = i.Id,
                Name = i.Name,
                Permissions = i.RolePermissions.Select(rp => new PermissionQRY
                {
                    Id = rp.Permission.Id,
                    Name = rp.Permission.Name
                }).ToList(),
                UpdatedAt = i.UpdatedAt,
                UpdatedBy = i.UpdatedBy,
            })
            .ToListAsync();

            return result;
        }
        public Task UpdateRoleAsync(Role role)
        {
            _context.Roles.Update(role);
            return Task.CompletedTask;
        }
        public Task DeleteRolePermissionsAsync(int roleId)
        {
            var records = _context.RolePermissions.Where(i => i.RoleId == roleId).ToList();
            if (records.Any())
            {
                _context.RolePermissions.RemoveRange(records);
            }
            return Task.CompletedTask;
        }
        public Task DeleteRoleAsync(Role role)
        {
            _context.Roles.Remove(role);
            return Task.CompletedTask;
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
        public async Task<List<UserWithRolesQRY>> GetAllUsersWithRoles()
        {
            var result = await _context.Users
                .AsNoTracking()
                 .Where(u => u.Id != 1)
                .Select(u => new UserWithRolesQRY
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
                        Name = ur.Role.Name
                    }).ToList(),
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
        public Task DeleteUserRolesByUserIdAsync(int userId)
        {
            var records = _context.UserRoles.Where(i => i.UserId == userId).ToList();
            if (records.Any())
            {
                _context.UserRoles.RemoveRange(records);
            }
            return Task.CompletedTask;
        }
    }
}


