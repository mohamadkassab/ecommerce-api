using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using ecommerce_dash_api.Repositories;
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

        public async Task<bool> UpdateUserRolesAsync(UserRolesUpdateDTO updateUserRolesDto)
        {
            await _userRepository.DeleteUserRolesAsync(updateUserRolesDto.UserId);
            await _userRepository.CreateUserRolesAsync(updateUserRolesDto.UserId, updateUserRolesDto.RoleIds);
            await _context.SaveChangesAsync();

            return true;
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
        
        public async Task<bool> SignupAsync(UserCreateDTO userDto)
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
            user.Dob = userDto.Dob;
            user.PasswordHash = _passwordHasher.HashPassword(userDto.Password);
            await _userRepository.CreateUserAsync(user);
            await _context.SaveChangesAsync();
            await _userRepository.DeleteUserRolesAsync(user.Id);
            await _userRepository.CreateUserRolesAsync(user.Id, userDto.Roles);
            await _context.SaveChangesAsync();

            return true;
        }

 
        public async Task<bool> CreateRoleAsync(RoleCreateDTO createRoleDto)
        {
            Role role = new Role();
            role.RoleName = createRoleDto.RoleName;
            await _userRepository.CreateRoleAsync(role, createRoleDto.PermissionIds);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRoleAsync(RoleUpdateDTO updateRoleDto)
        {
            await _userRepository.DeleteRolePermissionsAsync(updateRoleDto.RoleId);
            await _userRepository.CreateRolePermissionsAsync(updateRoleDto.RoleId, updateRoleDto.PermissionIds);
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

        public async Task<bool> DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteUserAsync(userId);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

