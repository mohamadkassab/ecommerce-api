using ecommerce_dash_api.Data;
using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ecommerce_dash_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly JwtToken _jwtToken;

        public UserController(IUserService userService, JwtToken jwtToken)
        {
            _userService = userService;
            _jwtToken = jwtToken;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _userService.SignupAsync(request);
                if (result)
                {
                    return Ok(new { message = "Signup successful" });
                }

                return BadRequest(new { message = "Signup failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] LoginDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var token = await _userService.SigninAsync(request);
                if (token != null)
                {
                    return Ok(new { Token = token });
                }

                return BadRequest(new { message = "Signin failed" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("updateUserRoles")]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

        
                    var response = await _userService.UpdateUserRolesAsync(request.UserId, request.RoleIds);
                    if (response)
                    {
                        return Ok();
                    }

                    return BadRequest(new { message = "Update user roles failed" });
            
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _userService.CreateRoleAsync(request.RoleName, request.PermissionIds);
                if (response)
                {
                    return Ok();
                }

                return BadRequest(new { message = "Create role failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("updateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _userService.UpdateRoleAsync(request.RoleId, request.PermissionIds);
                if (response)
                {
                    return Ok();
                }

                return BadRequest(new { message = "Update role failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("deleteRole")]
        public async Task<IActionResult> DeleteRole([FromBody] int roleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _userService.DeleteRoleAsync(roleId);
                if (response)
                {
                    return Ok();
                }

                return BadRequest(new { message = "Delete role failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("test")]
        [Authorize]
        public async Task<IActionResult> test()
        {
            try
            {

                return Ok("hello world");
                var userClaims = User.Claims;
                if (userClaims.Any(c => c.Type == "permission" && c.Value == "x"))
                {
                    // User has the permission, proceed with the action
                    return Ok("Access granted.");
                }
                else
                {
                    // User does not have the permission, return forbidden
                    return Forbid();
                }

                if (User.IsInRole("Admin"))
                {
                    // User is an Admin, proceed with the action
                    return Ok("Action performed successfully!");
                }





            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getAllRolesAndPermissions")]
        public async Task<IActionResult> GetAllRolesAndPermissions()
        {
            try
            {
                var result = await _userService.GetAllRolesWithPermissionsAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getAllUsersWithRolesAndPermissions")]
        public async Task<IActionResult> GetAllUsersWithRolesAndPermissions()
        {
            try
            {
                var result = await _userService.GetAllUsersWithRolesAndPermissions();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getAllPermissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            try
            {
                var result = await _userService.GetAllPermissionsAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
