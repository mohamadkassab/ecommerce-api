using ecommerce_dash_api.Areas.Dashboard.DTOS;
using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ecommerce_dash_api.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Route("Dashboard/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = nameof(UserTypeEnum.DashboardUser))]
    public class KpiController : Controller
    {
        private readonly IKpiService _kpiService;
        public readonly IApiService _apiService;
        public KpiController(IKpiService kpiService, IApiService apiService)
        {
            _kpiService = kpiService;
            _apiService = apiService;
        }

        //+------------------------------------------------------------------+
        //| Kpi                                            
        //+------------------------------------------------------------------+
        [HttpGet]
        public async Task<IActionResult> GetAllACharts()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null; 
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "chart_r") || (username == "root@e.com"))
            {
                try
                {
                    var response = await _kpiService.GetAllChartsAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, null);
                    return Ok(response);
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

        [HttpGet]
        public async Task<IActionResult> GetChartDataByQuery([FromQuery] string query)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "chart_r") || (username == "root@e.com"))
            {
                try
                {
                    var response = await _kpiService.GetChartDataByQueryAsync(query);
                    var jsonResponse = JsonConvert.SerializeObject(response);
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, query);
                    return Ok(jsonResponse);
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, query);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateChart([FromBody] ChartCreateDTO chart)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "chart_crud") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, chart);
                        return BadRequest(ModelState);
                    }

                    var response = await _kpiService.CreateChartAsync(chart, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, chart);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, chart);
                    return BadRequest(new { message = "Create chart failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, chart);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateChart([FromBody] ChartUpdateDTO chart)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "chart_crud") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, chart);
                        return BadRequest(ModelState);
                    }

                    var response = await _kpiService.UpdateChartAsync(chart, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, chart);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, chart);
                    return BadRequest(new { message = "Update chart failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, chart);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChart(int id)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "chart_crud") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, id);
                        return BadRequest(ModelState);
                    }

                    var response = await _kpiService.DeleteChartAsync(id);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, id);
                        return Ok();
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, id);
                    return BadRequest(new { message = "Delete chart failed" });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, id);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }
    }
}
