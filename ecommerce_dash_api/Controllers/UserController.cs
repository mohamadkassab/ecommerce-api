using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Enum;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Utils;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace ecommerce_dash_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public readonly IApiService _apiService;

        public UserController(IUserService userService, IConfiguration configuration, IApiService apiService)
        {
            _userService = userService;
            _configuration = configuration;
            _apiService = apiService;
        }


        //+------------------------------------------------------------------+
        //| Permission                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllPermissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "role_crud") || (username == "root@e.com"))

            {
                try
                {
                    var result = await _userService.GetAllPermissionsAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, null);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, null);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        //+------------------------------------------------------------------+
        //| Role                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllRolesAndPermissions")]
        public async Task<IActionResult> GetAllRolesAndPermissions()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && (c.Value == "user_crud" || c.Value == "role_crud")) || (username == "root@e.com"))

            {
                try
                {
                    var result = await _userService.GetAllRolesWithPermissionsAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, null);
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, null);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateDTO role)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "role_crud") || (username == "root@e.com"))

            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, role);
                        return BadRequest(ModelState);
                    }

                    var response = await _userService.CreateRoleAsync(role, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, role);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, role);
                    return BadRequest(new { message = "Create role failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, role);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleUpdateDTO role)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "role_crud") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, role);
                        return BadRequest(ModelState);
                    }

                    var response = await _userService.UpdateRoleAsync(role, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, role);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, role);
                    return BadRequest(new { message = "Update role failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex.StackTrace, username, ipAddress, actionName, role);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;

            if (userClaims.Any(c => c.Type == "permission" && c.Value == "role_crud") || (username == "root@e.com"))

            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _userService.DeleteRoleAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete role failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex.InnerException.Message : ex.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        //+------------------------------------------------------------------+
        //| User                                            
        //+------------------------------------------------------------------+
        [HttpPost("Signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Signin([FromBody] SigninDTO credentials)
        {
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            try
            {
                if (!ModelState.IsValid)
                {
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, null, ipAddress, actionName, credentials);
                    return BadRequest(ModelState);
                }

                var token = await _userService.SigninAsync(credentials);
                if (token != null)
                {
                    var jwtSettings = _configuration.GetSection("JwtSettings");
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtSettings["ExpirationInMinutes"])),
                    };
                    Response.Cookies.Append(jwtSettings["TokenName"]!, token, cookieOptions);

                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, null, ipAddress, actionName, credentials.Username);
                    return Ok(new { token });
                }
                await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, null, ipAddress, actionName, credentials.Username);
                return BadRequest(new { message = LogMessageTemplatesEnum.failed.ToString() });

            }
            catch (Exception ex)
            {
                string errorMessage = $"Error: {ex.Message}";
                string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, null, ipAddress, actionName, credentials);
                return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
            }
        }

        [HttpPost("Signout")]
        public async Task<IActionResult> Signout()
        {
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                Response.Cookies.Delete(jwtSettings["TokenName"]!);
                await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, null, ipAddress, actionName, User?.Identity?.Name);

                return Ok(new { message = LogMessageTemplatesEnum.successful.ToString() });
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error: {ex.Message}";
                string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, null, ipAddress, actionName, User?.Identity?.Name);
                return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
            }
        }

        [HttpGet("GetAllUsersWithRoles")]
        public async Task<IActionResult> GetAllUsersWithRoles()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "user_crud") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _userService.GetAllUsersWithRoles();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, null);
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, null);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO user)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;

            if ((userClaims.Any(c => c.Type == "permission" && c.Value == "user_crud") || (username == "root@e.com")) && user.UserName != "root@e.com")

            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, user);
                        return BadRequest(ModelState);
                    }

                    var result = await _userService.CreateUserAsync(user, username);
                    user.Password = null;
                    if (result)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, user);
                        return Ok(new { message = LogMessageTemplatesEnum.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, user);
                    return BadRequest(new { message = LogMessageTemplatesEnum.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, user);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO user)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if ((userClaims.Any(c => c.Type == "permission" && c.Value == "user_crud") || (username == "root@e.com")) && user.Id != 1)
            {
                try
                {
                    if (!ModelState.IsValid || (user?.Password?.Length < 6 && !string.IsNullOrEmpty(user.Password)))
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, user);
                        return BadRequest(ModelState);
                    }

                    var result = await _userService.UpdateUserAsync(user, username);
                    user.Password = null;
                    if (result)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, user);
                        return Ok(new { message = LogMessageTemplatesEnum.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, user);
                    return BadRequest(new { message = LogMessageTemplatesEnum.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, user);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            try
            {
                if (!ModelState.IsValid)
                {
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, changePassword);
                    return BadRequest(ModelState);
                }

                var response = await _userService.ChangePasswordAsync(changePassword, username);
                if (response)
                {
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, null);
                    return Ok();
                }
                await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, changePassword);
                return BadRequest(new { message = "Change password failed" });
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error: {ex.Message}";
                string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, changePassword);
                return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
            }
        }
    }
}
