using ecommerce_dash_api.Areas.Dashboard.DTOS;
using ecommerce_dash_api.Areas.Dashboard.QRYS;

namespace ecommerce_dash_api.Areas.Dashboard.Interfaces
{
    public interface IUserService
    {
        //+------------------------------------------------------------------+
        //| Permission                                            
        //+------------------------------------------------------------------+
        Task<List<PermissionQRY>> GetAllPermissionsAsync();

        //+------------------------------------------------------------------+
        //| Role                                            
        //+------------------------------------------------------------------+
        Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync();
        Task<bool> CreateRoleAsync(RoleCreateDTO createRoleDto, string? username);
        Task<bool> UpdateRoleAsync(RoleUpdateDTO updateRoleDto, string? username);
        Task<bool> DeleteRoleAsync(int roleId);

        //+------------------------------------------------------------------+
        //| User                                            
        //+------------------------------------------------------------------+
        Task<List<UserWithRolesQRY>> GetAllUsersWithRoles();
        Task<string> SigninAsync(SigninDTO loginDto);
        Task<bool> CreateUserAsync(UserCreateDTO userDto, string? username);
        Task<bool> UpdateUserAsync(UserUpdateDTO userDto, string? username);
        Task<bool> ChangePasswordAsync(ChangePasswordDTO changePassword, string? username);
    }
}
