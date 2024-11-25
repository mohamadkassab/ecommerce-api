using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Enum;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Services;
using ecommerce_dash_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ecommerce_dash_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class SetupController : Controller
    {
        private readonly ISetupService _setupService;
        private readonly IConfiguration _configuration;
        public readonly IApiService _apiService;
        private readonly JwtToken _jwtToken;

        public SetupController(ISetupService setupService, IConfiguration configuration, IApiService apiService, JwtToken jwtToken)
        {
            _setupService = setupService;
            _configuration = configuration;
            _apiService = apiService;
            _jwtToken = jwtToken;
        }

        //+------------------------------------------------------------------+
        //| Country                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllCountries")]
        public async Task<IActionResult> GetAllCountries()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "country") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _setupService.GetAllCountriesAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, null);
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

        [HttpPost("CreateCountry")]
        public async Task<IActionResult> CreateCountry([FromBody] CountryCreateDTO country)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "country") || (username == "root@e.com"))

            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, country);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.CreateCountryAsync(country, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, country);
                        return Ok(new { message = LogMessageTemplates.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, country);
                    return BadRequest(new { message = LogMessageTemplates.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, country);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateCountry")]
        public async Task<IActionResult> UpdateCountry([FromBody] CountryUpdateDTO country)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "country") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, country);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.UpdateCountryAsync(country, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, country);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, country);
                    return BadRequest(new { message = "Update failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, country);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("DeleteCountry")]
        public async Task<IActionResult> DeleteCountry([FromBody] int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "country") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.DeleteCountryAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex.InnerException?.Message : ex.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }


        //+------------------------------------------------------------------+
        //| Brand                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllBrands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "brand") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _setupService.GetAllBrandsAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, null);
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

        [HttpPost("CreateBrand")]
        public async Task<IActionResult> CreateBrand([FromBody] BrandCreateDTO brand)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "brand") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, brand);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.CreateBrandAsync(brand, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, brand);
                        return Ok(new { message = LogMessageTemplates.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, brand);
                    return BadRequest(new { message = LogMessageTemplates.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, brand);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateBrand")]
        public async Task<IActionResult> UpdateBrand([FromBody] BrandUpdateDTO brand)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "brand") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, brand);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.UpdateBrandAsync(brand, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, brand);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, brand);
                    return BadRequest(new { message = "Update failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, brand);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("DeleteBrand")]
        public async Task<IActionResult> DeleteBrand([FromBody] int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "brand") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.DeleteBrandAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex.InnerException?.Message : ex.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }


        //+------------------------------------------------------------------+
        //| Category                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "category") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _setupService.GetAllCategoriesAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, null);
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

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO category)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "category") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, category);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.CreateCategoryAsync(category, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, category);
                        return Ok(new { message = LogMessageTemplates.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, category);
                    return BadRequest(new { message = LogMessageTemplates.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, category);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDTO category)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "category") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, category);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.UpdateCategoryAsync(category, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, category);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, category);
                    return BadRequest(new { message = "Update failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, category);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "category") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.DeleteCategoryAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex.InnerException?.Message : ex.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }


        //+------------------------------------------------------------------+
        //| Currency                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllCurrencies")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "currency") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _setupService.GetAllCurrenciesAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, null);
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

        [HttpPost("CreateCurrency")]
        public async Task<IActionResult> CreateCurrency([FromBody] CurrencyCreateDTO currency)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "currency") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, currency);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.CreateCurrencyAsync(currency, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, currency);
                        return Ok(new { message = LogMessageTemplates.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, currency);
                    return BadRequest(new { message = LogMessageTemplates.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, currency);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateCurrency")]
        public async Task<IActionResult> UpdateCurrency([FromBody] CurrencyUpdateDTO currency)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "currency") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, currency);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.UpdateCurrencyAsync(currency, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, currency);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, currency);
                    return BadRequest(new { message = "Update failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, currency);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("DeleteCurrency")]
        public async Task<IActionResult> DeleteCurrency([FromBody] int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "currency") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.DeleteCurrencyAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }


        //+------------------------------------------------------------------+
        //| Year                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllYears")]
        public async Task<IActionResult> GetAllYears()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "year") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _setupService.GetAllYearsAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, null);
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

        [HttpPost("CreateYear")]
        public async Task<IActionResult> CreateYear([FromBody] YearCreateDTO year)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "year") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, year);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.CreateYearAsync(year, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, year);
                        return Ok(new { message = LogMessageTemplates.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, year);
                    return BadRequest(new { message = LogMessageTemplates.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, year);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateYear")]
        public async Task<IActionResult> UpdateYear([FromBody] YearUpdateDTO year)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "year") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, year);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.UpdateYearAsync(year, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, year);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, year);
                    return BadRequest(new { message = "Update failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, year);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("DeleteYear")]
        public async Task<IActionResult> DeleteYear([FromBody] int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "season") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.DeleteYearAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }


        //+------------------------------------------------------------------+
        //| Season                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllSeasons")]
        public async Task<IActionResult> GetAllSeasons()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "season") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _setupService.GetAllSeasonsAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, null);
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

        [HttpPost("CreateSeason")]
        public async Task<IActionResult> CreateSeason([FromBody] SeasonCreateDTO season)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "season") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, season);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.CreateSeasonAsync(season, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, season);
                        return Ok(new { message = LogMessageTemplates.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, season);
                    return BadRequest(new { message = LogMessageTemplates.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, season);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateSeason")]
        public async Task<IActionResult> UpdateSeason([FromBody] SeasonUpdateDTO season)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "season") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, season);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.UpdateSeasonAsync(season, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, season);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, season);
                    return BadRequest(new { message = "Update failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, season);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("DeleteSeason")]
        public async Task<IActionResult> DeleteSeason([FromBody] int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "season") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.DeleteSeasonAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }


        //+------------------------------------------------------------------+
        //| Section                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllSections")]
        public async Task<IActionResult> GetAllSections()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "section") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _setupService.GetAllSectionsWithCategoriesAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, null);
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

        [HttpPost("CreateSection")]
        public async Task<IActionResult> CreateSection([FromBody] SectionCreateDTO section)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "section") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, section);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.CreateSectionAsync(section, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, section);
                        return Ok(new { message = LogMessageTemplates.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, section);
                    return BadRequest(new { message = LogMessageTemplates.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, section);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateSection")]
        public async Task<IActionResult> UpdateSection([FromBody] SectionUpdateDTO section)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "section") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, section);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.UpdateSectionAsync(section, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, section);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, section);
                    return BadRequest(new { message = "Update failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, section);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("DeleteSection")]
        public async Task<IActionResult> DeleteSection([FromBody] int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "section") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.DeleteSectionAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }


        //+------------------------------------------------------------------+
        //| Supplier                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllSuppliers")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "supplier") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _setupService.GetAllSuppliersAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, null);
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

        [HttpPost("CreateSupplier")]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierCreateDTO supplier)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "supplier") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, supplier);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.CreateSupplierAsync(supplier, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, supplier);
                        return Ok(new { message = LogMessageTemplates.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, supplier);
                    return BadRequest(new { message = LogMessageTemplates.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, supplier);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier([FromBody] SupplierUpdateDTO supplier)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "supplier") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, supplier);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.UpdateSupplierAsync(supplier, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, supplier);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, supplier);
                    return BadRequest(new { message = "Update failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, supplier);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("DeleteSupplier")]
        public async Task<IActionResult> DeleteSupplier([FromBody] int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "supplier") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.DeleteSupplierAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }


        //+------------------------------------------------------------------+
        //| Tag                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllTags")]
        public async Task<IActionResult> GetAllTags()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "tag") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _setupService.GetAllTagsAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, null);
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

        [HttpPost("CreateTag")]
        public async Task<IActionResult> CreateTag([FromBody] TagCreateDTO tag)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "tag") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, tag);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.CreateTagAsync(tag, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, tag);
                        return Ok(new { message = LogMessageTemplates.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, tag);
                    return BadRequest(new { message = LogMessageTemplates.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, tag);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateTag")]
        public async Task<IActionResult> UpdateTag([FromBody] TagUpdateDTO tag)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "tag") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, tag);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.UpdateTagAsync(tag, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, tag);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, tag);
                    return BadRequest(new { message = "Update failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, tag);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("DeleteTag")]
        public async Task<IActionResult> DeleteTag([FromBody] int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "tag") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.DeleteTagAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }


        //+------------------------------------------------------------------+
        //| Attribute                                            
        //+------------------------------------------------------------------+
        [HttpGet("GetAllAttributesWithOptions")]
        public async Task<IActionResult> GetAllAttributesWithOptions()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "attribute") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _setupService.GetAllAttributesWithOptionsAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, null);
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

        [HttpPost("CreateAttribute")]
        public async Task<IActionResult> CreateAttribute([FromBody] AttributeCreateDTO attribute)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "attribute") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, attribute);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.CreateAttributeAsync(attribute, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, attribute);
                        return Ok(new { message = LogMessageTemplates.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, attribute);
                    return BadRequest(new { message = LogMessageTemplates.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, attribute);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("UpdateAttribute")]
        public async Task<IActionResult> UpdateAttribute([FromBody] AttributeUpdateDTO attribute)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "attribute") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, attribute);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.UpdateAttributeAsync(attribute, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, attribute);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, attribute);
                    return BadRequest(new { message = "Update failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, attribute);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("DeleteAttribute")]
        public async Task<IActionResult> DeleteAttribute([FromBody] int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "attribute") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _setupService.DeleteAttributeAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplates.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplates.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }
    }
}

