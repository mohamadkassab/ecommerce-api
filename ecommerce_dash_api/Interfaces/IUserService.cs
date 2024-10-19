
using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using Microsoft.AspNetCore.Identity.Data;

namespace ecommerce_dash_api.Interfaces
{
    public interface IUserService
    {
        Task<bool> SignupAsync(UserCreateDTO userDto);
        Task<string> SigninAsync(SigninDTO loginDto);
        Task<bool> UpdateUserRolesAsync(UserRolesUpdateDTO updateUserRolesDto);
        Task<bool> CreateRoleAsync(RoleCreateDTO createRoleDto);
        Task<bool> UpdateRoleAsync(RoleUpdateDTO updateRoleDto);
        Task<bool> DeleteRoleAsync(int roleId);
        Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync();
        Task<List<PermissionQRY>> GetAllPermissionsAsync();
        Task<List<UserWithRolesAndPermissionsQRY>> GetAllUsersWithRolesAndPermissions();
        Task<bool> DeleteUserAsync(int userId);


    }
}
