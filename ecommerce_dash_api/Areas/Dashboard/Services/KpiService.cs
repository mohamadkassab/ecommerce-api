using ecommerce_dash_api.Areas.Dashboard.DTOS;
using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Areas.Dashboard.QRYS;
using ecommerce_dash_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ecommerce_dash_api.Areas.Dashboard.Services
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

        //+------------------------------------------------------------------+
        //| Kpi                                            
        //+------------------------------------------------------------------+
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
        public async Task<bool> CreateChartAsync(ChartCreateDTO createChartDto, string? username)
        {
            var chart = new Chart { Label = createChartDto.Label, Query = createChartDto?.Query, Type = createChartDto.ChartType };
            await _context.Charts.AddAsync(chart);

            foreach (var chartpropertydto in createChartDto?.ChartProperties)
            {
                var charproperty = new ChartProperty
                {
                    Chart = chart,
                    Name = chartpropertydto.PropertyName,
                    Value = chartpropertydto.PropertyValue,
                    UpdatedBy = username
                };

                await _kpiRepository.CreateChartAsync(charproperty);
            }
            await _context.SaveChangesAsync();
            return true;   
        }
        public async Task<bool> UpdateChartAsync(ChartUpdateDTO updateChartDto, string? username)
        {
            await _kpiRepository.UpdateChartAsync(updateChartDto.Id, updateChartDto.Label, updateChartDto?.Query, updateChartDto?.ChartProperties, username);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteChartAsync(int chartId)
        {
            var chart = await _context.Charts
            .Where(c => c.Id == chartId)
            .FirstOrDefaultAsync();
            await _kpiRepository.DeleteChartAsync(chart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
