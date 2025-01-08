using ecommerce_dash_api.Areas.Dashboard.DTOS;
using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Areas.Dashboard.QRYS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_dash_api.Areas.Dashboard.Services
{
    public class UserService : IUserService
    {
        private readonly EcommerceContext _context;
        private readonly IUserRepository _userRepository;
        private readonly JwtToken _jwtToken;
        public UserService(IUserRepository userRepository, JwtToken jwtToken, EcommerceContext context)
        {
            _userRepository = userRepository;
            _jwtToken = jwtToken;
            _context = context;
        }

        //+------------------------------------------------------------------+
        //| Permission                                            
        //+------------------------------------------------------------------+
        public async Task<List<PermissionQRY>> GetAllPermissionsAsync()
        {
            var result = await _userRepository.GetAllPermissionsAsync();
            return result;
        }

        //+------------------------------------------------------------------+
        //| Role                                            
        //+------------------------------------------------------------------+
        public async Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync()
        {
            var result = await _userRepository.GetAllRolesWithPermissionsAsync();
            return result;
        }
        public async Task<bool> CreateRoleAsync(RoleCreateDTO createRoleDto, string? username)
        {
            Role role = new Role
            {
                Name = createRoleDto.Name,
                UpdatedBy = username,
            };
            var rolePermissions = createRoleDto.Permissions.Select(permissionId => new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permissionId,
                UpdatedBy = username
            }).ToList();
            role.RolePermissions = rolePermissions;
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateRoleAsync(RoleUpdateDTO updateRoleDto, string? username)
        {
            var deleteRolePermissions = _userRepository.DeleteRolePermissionsAsync(updateRoleDto.Id);
            var getRole = _userRepository.GetRoleByIdAsync(updateRoleDto.Id);
            Task.WhenAll(deleteRolePermissions, getRole);
            Role role = getRole.Result;
            role.Name = updateRoleDto.Name;
            role.UpdatedBy = username;
            var rolePermissions = updateRoleDto.Permissions.Select(permissionId => new RolePermission
            {
                RoleId = updateRoleDto.Id,
                PermissionId = permissionId,
                UpdatedBy = username
            }).ToList();
            role.RolePermissions = rolePermissions;
            await _userRepository.UpdateRoleAsync(role);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            var role = await _context.Roles
            .Where(r => r.Id == roleId)
            .FirstOrDefaultAsync();
            await _userRepository.DeleteRoleAsync(role);
            await _context.SaveChangesAsync();
            return true;
        }

        //+------------------------------------------------------------------+
        //| User                                            
        //+------------------------------------------------------------------+
        public async Task<List<UserWithRolesQRY>> GetAllUsersWithRoles()
        {
            var result = await _userRepository.GetAllUsersWithRoles();
            return result;
        }
        public async Task<string> SigninAsync(SigninDTO signinDTO)
        {
            var result = await _userRepository.GetUserByUsernameAsync(signinDTO.Username);
            if (result.user == null || !BCrypt.Net.BCrypt.Verify(signinDTO.Password, result.user.PasswordHash))
            {
                return null;
            }

            var token = await _jwtToken.GenerateJwtTokenAsync(result.user.Username, result.roles, result.permissions);

            return token;

        }
        public async Task<bool> CreateUserAsync(UserCreateDTO userDto, string? username)
        {
            if (await _userRepository.UsernameExistsAsync(userDto.UserName))
            {
                throw new InvalidOperationException("Email is already registered");
            }

            var hashedPassword = await Helpers.HashPasswordAsync(userDto.Password);
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Phone = userDto.Phone,
                Username = userDto.UserName,
                Dob = userDto.Dob,
                PasswordHash = hashedPassword,
                Address = userDto.Address,
                IsActive = userDto.IsActive,
                UpdatedBy = username
            };
            var userRoles = userDto.Roles.Select(roleId => new UserRole
            {
                UserId = user.Id,
                RoleId = roleId,
                UpdatedBy = username
            }).ToList();

            user.UserRoleUsers = userRoles;
            await _userRepository.CreateUserAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateUserAsync(UserUpdateDTO? userDto, string? username)
        {
            var deleteUserRolesTask = _userRepository.DeleteUserRolesByUserIdAsync(userDto.Id);
            var getUserTask = _userRepository.GetUserAsync(userDto.Id);
            Task.WhenAll(deleteUserRolesTask, getUserTask);
            User user = getUserTask.Result;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Dob = userDto.Dob;
            user.Phone = userDto.Phone;
            user.Address = userDto.Address;
            user.IsActive = userDto.IsActive;
            user.UpdatedBy = username;
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.PasswordHash = await Helpers.HashPasswordAsync(userDto.Password);
            }
            var userRoles = userDto.Roles.Select(roleId => new UserRole
            {
                UserId = user.Id,
                RoleId = roleId,
                UpdatedBy = username
            }).ToList();
            user.UserRoleUsers = userRoles;
            await _userRepository.UpdateUserAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ChangePasswordAsync(ChangePasswordDTO changePassword, string? username)
        {
            if (changePassword.NewPassword != changePassword.ConfirmNewPassword)
            {
                return false;
            }
            var (user, roles, permissions) = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(changePassword.OldPassword, user.PasswordHash))
            {
                return false;
            }
            user.PasswordHash = await Helpers.HashPasswordAsync(changePassword.NewPassword);
            await _userRepository.UpdateUserAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

