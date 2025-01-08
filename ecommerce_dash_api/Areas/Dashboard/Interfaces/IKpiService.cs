using ecommerce_dash_api.Areas.Dashboard.DTOS;
using ecommerce_dash_api.Areas.Dashboard.QRYS;
using System.Data;

namespace ecommerce_dash_api.Areas.Dashboard.Interfaces
{
    public interface IKpiService
    {
        //+------------------------------------------------------------------+
        //| Kpi                                            
        //+------------------------------------------------------------------+
        Task<List<ChartQRY>> GetAllChartsAsync();
        Task<DataTable> GetChartDataByQueryAsync(string query);
        Task<bool> CreateChartAsync(ChartCreateDTO createChartDto, string? username);
        Task<bool> UpdateChartAsync(ChartUpdateDTO updateChartDto, string? username);
        Task<bool> DeleteChartAsync(int chartId);
    }
}
