using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.QRYS;
using System.Data;

namespace ecommerce_dash_api.Interfaces
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
