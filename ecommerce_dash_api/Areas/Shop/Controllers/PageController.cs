using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Enum;
using ecommerce_dash_api.Utils;
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
        public readonly Interfaces.IPageService _pageService;
        public PageController(Interfaces.IPageService pageService, IConfiguration configuration, IApiService apiService)
        {
            _configuration = configuration;
            _apiService = apiService;
            _pageService = pageService;
        }

        //+------------------------------------------------------------------+
        //| Home                                            
        //+------------------------------------------------------------------+
        [HttpGet]
        public async Task<IActionResult> GetHomePageAssets()
        {
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            try
            {
                var result = new PageAssetsQRY();
                result.SectionImage = await Helpers.GetFileByUrlAsync("C:\\Users\\mhmdk\\Desktop\\Projects\\ecommerce-api\\ecommerce_dash_api\\wwwroot\\images\\section-main-images\\home\\home.jpg");
                result.SectionImageLink = "New collection";
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

        [HttpGet]
        public async Task<IActionResult> GetHomePageProductsAndBrands()
        {
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            try
            {
                var result = await _pageService.GetHomePageProductsAndBrandsAsync(1, 18);
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

        //+------------------------------------------------------------------+
        //| Products Search                                            
        //+------------------------------------------------------------------+
        [HttpGet("{categoryId}/{pageNbr}/{pageSize}")]
        public async Task<IActionResult> GetProductsByCategoryAndPage(int categoryId, int pageNbr, int pageSize)
        {
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            try
            {
                var result = await _pageService.GetProductsByCategoryAndPageAsync(categoryId, pageNbr, pageSize);
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
