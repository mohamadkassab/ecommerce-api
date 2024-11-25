using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using System.Data;

namespace ecommerce_dash_api.Interfaces
{
    public interface IKpiRepository
    {
        //+------------------------------------------------------------------+
        //| Kpi                                            
        //+------------------------------------------------------------------+
        Task<List<ChartQRY>> GetAllChartsAsync();
        Task<DataTable> GetChartDataByQueryAsync(string query);
        Task CreateChartAsync(ChartProperty charproperty);
        Task UpdateChartAsync(int id, string label, string query, List<ChartPropertyDTO> chartPropertiesDto, string username);
        Task DeleteChartAsync(Chart? chart);
    }
}
