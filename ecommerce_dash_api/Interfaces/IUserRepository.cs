using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;

namespace ecommerce_dash_api.Interfaces
{
    public interface IUserRepository
    {
        //+------------------------------------------------------------------+
        //| User                                            
        //+------------------------------------------------------------------+
        Task<User?> GetUserAsync(int id);
        Task<(User? user, List<string>? roles, List<string>? permissions)> GetUserByUsernameAsync(string username);
        Task<List<UserWithRolesQRY>> GetAllUsersWithRoles();
        Task<bool> UsernameExistsAsync(string username);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);  
        Task DeleteUserAsync(User user);
        Task DeleteUserRolesByUserIdAsync(int userId);


        //+------------------------------------------------------------------+
        //| Role                                            
        //+------------------------------------------------------------------+
        Task UpdateRoleAsync(Role role);
        Task<Role?> GetRoleByIdAsync(int id);
        Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync();
        Task DeleteRoleAsync(Role role);
        Task DeleteRolePermissionsAsync(int permissionId);


        //+------------------------------------------------------------------+
        //| Permission                                            
        //+------------------------------------------------------------------+
        Task<List<PermissionQRY>> GetAllPermissionsAsync();
    }
}
