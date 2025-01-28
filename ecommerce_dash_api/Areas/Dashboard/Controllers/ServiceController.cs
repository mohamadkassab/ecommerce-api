using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Enum;
using ecommerce_dash_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_dash_api.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Route("Dashboard/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = nameof(UserTypeEnum.DashboardUser))]

    public class ServiceController : Controller
    {
        public readonly IApiService _apiService;

        public ServiceController(IApiService apiService)
        {
            _apiService = apiService;
        }

        //+------------------------------------------------------------------+
        //| Log                                            
        //+------------------------------------------------------------------+
        [HttpPost]
        public async Task<IActionResult> CreateLog(ApiLog apiLog)
        {
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            try
            {
                apiLog.IpAddress = ipAddress;
                await _apiService.CreateLogAsync(apiLog);
                return CreatedAtAction(nameof(CreateLog), apiLog);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex?.Message });
            }
        }
    }
}
