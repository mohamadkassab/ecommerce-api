
using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;

namespace ecommerce_dash_api.Interfaces
{
    public interface IUserService
    {
        //+------------------------------------------------------------------+
        //| User                                            
        //+------------------------------------------------------------------+
        Task<List<UserWithRolesQRY>> GetAllUsersWithRoles();
        Task<string> SigninAsync(SigninDTO loginDto);
        Task<bool> CreateUserAsync(UserCreateDTO userDto, string? username);
        Task<bool> UpdateUserAsync(UserUpdateDTO userDto, string? username);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> ChangePasswordAsync(ChangePasswordDTO changePassword, string? username);


        //+------------------------------------------------------------------+
        //| Role                                            
        //+------------------------------------------------------------------+
        Task<List<RoleQRY>> GetAllRolesWithPermissionsAsync();
        Task<bool> CreateRoleAsync(RoleCreateDTO createRoleDto, string? username);
        Task<bool> UpdateRoleAsync(RoleUpdateDTO updateRoleDto, string? username);
        Task<bool> DeleteRoleAsync(int roleId);


        //+------------------------------------------------------------------+
        //| Permission                                            
        //+------------------------------------------------------------------+
        Task<List<PermissionQRY>> GetAllPermissionsAsync();
    }
}
