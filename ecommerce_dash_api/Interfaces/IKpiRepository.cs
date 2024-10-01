using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.QRYS;
using System.Data;

namespace ecommerce_dash_api.Interfaces
{
    public interface IKpiRepository
    {
        Task CreateChartAsync(string label, string query, string chartType, List<ChartPropertyDTO> chartPropertiesDto);
        Task<List<ChartQRY>> GetAllChartsAsync();

        Task UpdateChartAsync(int id, string label, string query, List<ChartPropertyDTO> chartPropertiesDto);

        Task DeleteChartAsync(int chartId);

        Task<DataTable> GetChartDataByQueryAsync(string query);

    }
}
