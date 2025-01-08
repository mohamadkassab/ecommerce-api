using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Enum;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace ecommerce_dash_api.Areas.Shop.Controllers
{
    [Area("Shop")]
    [Route("Shop/[controller]/[action]")]
    [ApiController]
    public class PageController : Controller
    {
        private readonly IConfiguration _configuration;
        public readonly IApiService _apiService;
        public readonly IPageService _pageService;
        public PageController(IPageService pageService, IConfiguration configuration, IApiService apiService)
        {
            _configuration = configuration;
            _apiService = apiService;
            _pageService = pageService;
        }

        //+------------------------------------------------------------------+
        //| Home                                            
        //+------------------------------------------------------------------+
        [HttpGet]
        public async Task<IActionResult> GetHomePage()
        {
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            try
            {
                var result = await _pageService.GetHomePageAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error: {ex.Message}";
                string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, null, ipAddress, actionName, null);
                return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
            }
        }
    }
}
