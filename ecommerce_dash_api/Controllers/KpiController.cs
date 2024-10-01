using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace ecommerce_dash_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KpiController : Controller
    {
        private readonly IKpiService _kpiService;
        public KpiController(IKpiService kpiService)
        {
            _kpiService = kpiService;
        }

        [HttpPost("createChart")]
        public async Task<IActionResult> CreateChart([FromBody] CreateChartDTO chartDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _kpiService.CreateChartAsync(chartDto);
                if (response)
                {
                    return Ok();
                }

                return BadRequest(new { message = "Create chart failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getAllCharts")]
        public async Task<IActionResult> GetAllgetACharts()
        {
            try
            {
                var response = await _kpiService.GetAllChartsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateChart")]
        public async Task<IActionResult> UpdateChart([FromBody] UpdateChartDTO chartDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _kpiService.UpdateChartAsync(chartDto);
                if (response)
                {
                    return Ok();
                }

                return BadRequest(new { message = "Update chart failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("deleteChart")]
        public async Task<IActionResult> DeleteChart([FromBody] int chartId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _kpiService.DeleteChartAsync(chartId);
                if (response)
                {
                    return Ok();
                }

                return BadRequest(new { message = "Delete chart failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getChartDataByQuery")]
        public async Task<IActionResult> GetChartDataByQuery([FromQuery] string query)
        {
            try
            {
                var response = await _kpiService.GetChartDataByQueryAsync(query);
                var jsonResponse = JsonConvert.SerializeObject(response);
                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
