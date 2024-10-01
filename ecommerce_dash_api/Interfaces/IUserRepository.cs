using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;

namespace ecommerce_dash_api.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<bool> UsernameExistsAsync(string username);
        Task<(User user, List<string> roles, List<string> permissions)> GetUserByUsernameAsync(string username);
        Task DeleteUserRolesAsync(int userId);
        Task CreateUserRolesAsync(int userId, List<int> roleIds);
        Task CreateRoleAsync(Role role, List<int> permissionIds);
        Task CreateRolePermissionsAsync(int roleId, List<int> permissionIds);
        Task DeleteRolePermissionsAsync(int roleId);
        Task DeleteRoleAsync(int roleId);
        Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync();
        Task<List<PermissionQRY>> GetAllPermissionsAsync();
        Task<List<UserWithRolesAndPermissionsQRY>> GetAllUsersWithRolesAndPermissions();


    }
}
