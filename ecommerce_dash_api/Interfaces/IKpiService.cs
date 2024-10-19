using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.QRYS;
using System.Data;

namespace ecommerce_dash_api.Interfaces
{
    public interface IKpiService
    {
        Task<bool> CreateChartAsync(ChartCreateDTO createChartDto);
        Task<List<ChartQRY>> GetAllChartsAsync();
        Task<bool> UpdateChartAsync(ChartUpdateDTO updateChartDto);
        Task<bool> DeleteChartAsync(int chartId);
        Task<DataTable> GetChartDataByQueryAsync(string query);
    }
}
