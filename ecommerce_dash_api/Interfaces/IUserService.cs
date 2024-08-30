
using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using Microsoft.AspNetCore.Identity.Data;

namespace ecommerce_dash_api.Interfaces
{
    public interface IUserService
    {
        Task<bool> SignupAsync(UserDTO user);
        Task<string> SigninAsync(LoginDTO request);
        Task<bool> UpdateUserRolesAsync(int userId, List<int> roleIds);
        Task<bool> CreateRoleAsync(string roleName, List<int> permissionIds);
        Task<bool> UpdateRoleAsync(int roleId, List<int> permissionIds);
        Task<bool> DeleteRoleAsync(int roleId);
        Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync();
        Task<List<PermissionQRY>> GetAllPermissionsAsync();
        Task<List<UserWithRolesAndPermissionsQRY>> GetAllUsersWithRolesAndPermissions();

    }
}
