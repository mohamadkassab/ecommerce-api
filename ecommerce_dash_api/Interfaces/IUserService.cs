
using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using Microsoft.AspNetCore.Identity.Data;

namespace ecommerce_dash_api.Interfaces
{
    public interface IUserService
    {
        Task<bool> SignupAsync(UserDTO userDto);
        Task<string> SigninAsync(SigninDTO loginDto);
        Task<bool> UpdateUserRolesAsync(UpdateUserRolesDTO updateUserRolesDto);
        Task<bool> CreateRoleAsync(CreateRoleDTO createRoleDto);
        Task<bool> UpdateRoleAsync(UpdateRoleDTO updateRoleDto);
        Task<bool> DeleteRoleAsync(int roleId);
        Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync();
        Task<List<PermissionQRY>> GetAllPermissionsAsync();
        Task<List<UserWithRolesAndPermissionsQRY>> GetAllUsersWithRolesAndPermissions();

    }
}
