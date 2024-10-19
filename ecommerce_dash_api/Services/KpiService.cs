using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ecommerce_dash_api.Services
{
    public class KpiService : IKpiService
    {
        private readonly EcommerceContext _context;
        private readonly IKpiRepository _kpiRepository;
        public KpiService(EcommerceContext context, IKpiRepository kpiRepository)
        {
            _context = context;
            _kpiRepository = kpiRepository;
        }
        public async Task<bool> CreateChartAsync(ChartCreateDTO createChartDto)
        {
            await _kpiRepository.CreateChartAsync(createChartDto.Label, createChartDto?.Query, createChartDto.ChartType, createChartDto?.ChartProperties);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteChartAsync(int chartId)
        {
            await _kpiRepository.DeleteChartAsync(chartId);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ChartQRY>> GetAllChartsAsync()
        {
            var result = await _kpiRepository.GetAllChartsAsync();
            return result;
        }

        public async Task<DataTable> GetChartDataByQueryAsync(string query)
        {
            var result = await _kpiRepository.GetChartDataByQueryAsync(query);
            return result;
        }

        public async Task<bool> UpdateChartAsync(ChartUpdateDTO updateChartDto)
        {
            await _kpiRepository.UpdateChartAsync(updateChartDto.Id, updateChartDto.Label, updateChartDto?.Query, updateChartDto?.ChartProperties);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
