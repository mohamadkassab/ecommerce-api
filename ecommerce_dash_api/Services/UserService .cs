using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using ecommerce_dash_api.Repositories;
using ecommerce_dash_api.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Collections.Specialized.BitVector32;


namespace ecommerce_dash_api.Services
{
    public class UserService : IUserService
    {
        private readonly EcommerceContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtToken _jwtToken;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, JwtToken jwtToken, EcommerceContext context)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtToken = jwtToken;
            _context = context;
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
            if (await _userRepository.UsernameExistsAsync(userDto.Username))
            {
                throw new InvalidOperationException("Email is already registered");
            }

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Phone = userDto.Phone,
                Username = userDto.Username,
                Dob = userDto.Dob,
                PasswordHash = _passwordHasher.HashPassword(userDto.Password),
                Address = userDto.Address,
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
            await _userRepository.DeleteUserRolesByUserIdAsync(userDto.Id);
            User user = await _userRepository.GetUserAsync(userDto.Id);
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Dob = userDto.Dob;
            user.Phone = userDto.Phone;
            user.Address = userDto.Address;
            user.UpdatedBy = username;
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(userDto.Password);
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
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users
              .Where(c => c.Id == userId)
              .FirstOrDefaultAsync();
            await _userRepository.DeleteUserAsync(user);
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
            user.PasswordHash = _passwordHasher.HashPassword(changePassword.NewPassword);
            await _userRepository.UpdateUserAsync(user);
            await _context.SaveChangesAsync();
            return true;
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
            await _userRepository.DeleteRolePermissionsAsync(updateRoleDto.Id);
            Role role = await _userRepository.GetRoleByIdAsync(updateRoleDto.Id);
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
        //| Permission                                            
        //+------------------------------------------------------------------+
        public async Task<List<PermissionQRY>> GetAllPermissionsAsync()
        {
            var result = await _userRepository.GetAllPermissionsAsync();
            return result;
        }
    }
}

