using ecommerce_dash_api.Enum;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ecommerce_dash_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class ServiceController : Controller
    {
        public readonly IApiService _apiService;

        public ServiceController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpPost("createLog")]
        public async Task<IActionResult> CreateLog(ApiLog apiLog)
        {
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            try
            {
                apiLog.IpAddress = ipAddress;
                await _apiService.CreateLogAsync(apiLog);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex?.Message });
            }
        }
    }
}
