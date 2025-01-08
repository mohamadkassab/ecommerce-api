using ecommerce_dash_api.Areas.Dashboard.QRYS;
using ecommerce_dash_api.Models;

namespace ecommerce_dash_api.Areas.Dashboard.Interfaces
{
    public interface IUserRepository
    {
        //+------------------------------------------------------------------+
        //| Permission                                            
        //+------------------------------------------------------------------+
        Task<List<PermissionQRY>> GetAllPermissionsAsync();

        //+------------------------------------------------------------------+
        //| Role                                            
        //+------------------------------------------------------------------+
        Task UpdateRoleAsync(Role role);
        Task<Role?> GetRoleByIdAsync(int id);
        Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync();
        Task DeleteRoleAsync(Role role);
        Task DeleteRolePermissionsAsync(int permissionId);

        //+------------------------------------------------------------------+
        //| User                                            
        //+------------------------------------------------------------------+
        Task<User?> GetUserAsync(int id);
        Task<(User? user, List<string>? roles, List<string>? permissions)> GetUserByUsernameAsync(string username);
        Task<List<UserWithRolesQRY>> GetAllUsersWithRoles();
        Task<bool> UsernameExistsAsync(string username);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserRolesByUserIdAsync(int userId);
    }
}
