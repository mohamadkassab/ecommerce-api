using ecommerce_dash_api.Data;
using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using ecommerce_dash_api.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        public async Task<bool> UpdateUserRolesAsync(int userId, List<int> roleIds)
        {
            await _userRepository.DeleteUserRolesAsync(userId);
            await _userRepository.CreateUserRolesAsync(userId, roleIds);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> SigninAsync(LoginDTO request)
        {
            var result = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (result.user == null || !BCrypt.Net.BCrypt.Verify(request.Password, result.user.PasswordHash))
            {
                return null;
            }
   
            var token = _jwtToken.GenerateJwtToken(result.user.Username, result.roles, result.permissions);

            return token;

        }
        
        public async Task<bool> SignupAsync(UserDTO userDto)
        {

            if (await _userRepository.UsernameExistsAsync(userDto.Username))
            {
                throw new InvalidOperationException("Email is already registered");
            }

            User user = new User();
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Phone = userDto.Phone;
            user.Username = userDto.Username;
            user.Age = userDto.Age;
            user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);

            await _userRepository.CreateUserAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateRoleAsync(string roleName, List<int> permissionIds)
        {
            Role role = new Role();
            role.RoleName = roleName;
            await _userRepository.CreateRoleAsync(role, permissionIds);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRoleAsync(int roleId, List<int> permissionIds)
        {
            await _userRepository.DeleteRolePermissionsAsync(roleId);
            await _userRepository.CreateRolePermissionsAsync(roleId, permissionIds);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            await _userRepository.DeleteRoleAsync(roleId);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync()
        {
            var result = await _userRepository.GetAllRolesWithPermissionsAsync();
            return result;
        }

        public async Task<List<PermissionQRY>> GetAllPermissionsAsync()
        {
            var result = await _userRepository.GetAllPermissionsAsync();
            return result;
        }

        public async Task<List<UserWithRolesAndPermissionsQRY>> GetAllUsersWithRolesAndPermissions()
        {
            var result = await _userRepository.GetAllUsersWithRolesAndPermissions();
            return result;
        }
    }
}

