using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using ecommerce_dash_api.Utils;
using Microsoft.EntityFrameworkCore;
using System;


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
        public async Task<List<UserWithRolesAndPermissionsQRY>> GetAllUsersWithRolesAndPermissionsAsync()
        {
            var result = await _userRepository.GetAllUsersWithRolesAndPermissions();
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

            User user = new User();
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Phone = userDto.Phone;
            user.Username = userDto.Username;
            user.Dob = userDto.Dob;
            user.PasswordHash = _passwordHasher.HashPassword(userDto.Password);
            user.UpdatedBy = username;
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
            User user = await _userRepository.GetUserAsync(userDto.Id);
            if (user != null)
            {
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

                var userRoles = await _context.UserRoles.Where(ur => ur.UserId == user.Id).ToListAsync();
                await _userRepository.DeleteUserRolesAsync(userRoles);
                userRoles = userDto.Roles.Select(roleId => new UserRole
                {
                    UserId = user.Id,
                    RoleId = roleId,
                    UpdatedBy = username
                }).ToList();
                await _userRepository.UpdateUserAsync(user);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;

            //public async Task AddUserWithRoles(AppDbContext context)
            //{
            //    // Create a new user
            //    var newUser = new User
            //    {
            //        Username = "john_doe",
            //        FirstName = "John",
            //        LastName = "Doe",
            //        DOB = new DateTime(1990, 1, 1),
            //        Phone = "1234567890",
            //        Address = "123 Main Street",
            //        PasswordHash = "hashed_password_here",
            //        UpdatedBy = "admin",
            //        FailedLoginAttempts = 0,
            //        IsActive = true
            //    };

            //    // Assign roles to the user
            //    var rolesToAssign = await context.Roles
            //        .Where(r => r.Name == "Admin" || r.Name == "Editor") // Example roles
            //        .ToListAsync();

            //    newUser.UserRoles = rolesToAssign.Select(r => new UserRole
            //    {
            //        RoleId = r.Id,
            //        UpdatedBy = "admin"
            //    }).ToList();

            //    // Add the user to the database
            //    context.Users.Add(newUser);
            //    await context.SaveChangesAsync();
            //}
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
            Role role = new Role();
            List<RolePermission> rolePermissions = new List<RolePermission>();
            role.Name = createRoleDto.RoleName;

            foreach (var permissionId in createRoleDto.Permissions)
            {
                var rolePermission = new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permissionId,
                    UpdatedBy = username
                };
                rolePermissions.Add(rolePermission);
            }
            role.RolePermissions = rolePermissions;
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> UpdateRoleAsync(RoleUpdateDTO updateRoleDto, string? username)
        {
            Role role = await _userRepository.GetRoleByIdAsync(updateRoleDto.Id);
            List<RolePermission> rolePermissions = new List<RolePermission>();
            role.Name = updateRoleDto.RoleName;
            role.UpdatedBy = username;

            var rolePermissionsToDelete = await _context.RolePermissions
            .Where(rp => rp.RoleId == updateRoleDto.Id)
            .ToListAsync();
            await _userRepository.DeleteRolePermissionsAsync(rolePermissionsToDelete);

            foreach (var permissionId in updateRoleDto.Permissions)
            {
                var rolePermission = new RolePermission
                {
                    RoleId = updateRoleDto.Id,
                    PermissionId = permissionId,
                    UpdatedBy = username
                };
                rolePermissions.Add(rolePermission);
            }
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

