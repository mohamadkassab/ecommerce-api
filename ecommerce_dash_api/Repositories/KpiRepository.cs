using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using Microsoft.EntityFrameworkCore;
using System.Data;
using MySql.Data.MySqlClient;

namespace ecommerce_dash_api.Repositories
{
    public class KpiRepository : IKpiRepository
    {
        private readonly EcommerceContext _context;
        private readonly IConfiguration _configuration;
        public KpiRepository(EcommerceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task CreateChartAsync(string label, string query, string chartType, List<ChartPropertyDTO> chartPropertiesDto)
        {
            var chart = new Chart { Label = label, Query = query, ChartType = chartType };
            await _context.Charts.AddAsync(chart);

            try
            {
                await _context.SaveChangesAsync();

                foreach (var chartpropertydto in chartPropertiesDto)
                {
                    var charproperty = new ChartProperty
                    {
                        ChartId = chart.Id,
                        PropertyName = chartpropertydto.PropertyName,
                        PropertyValue = chartpropertydto.PropertyValue
                    };

                    await _context.ChartProperties.AddAsync(charproperty);
                }
            }
            catch (Exception ex)
            {
                _context.Charts.Remove(chart);
                await _context.SaveChangesAsync();
                throw;
            }
        }

        public async Task DeleteChartAsync(int chartId)
        {
            var chart = await _context.Charts
            .Where(c => c.Id == chartId)
            .FirstOrDefaultAsync();
            _context.Charts.Remove(chart);
        }

        public Task<List<ChartQRY>> GetAllChartsAsync()
        {
            var result = _context.Charts.Select(c => new ChartQRY{
                Id = c.Id,
                Label = c.Label,
                Query = c.Query,
                ChartType = c.ChartType,
                ChartProperties = c.ChartProperties.Select(cp => new ChartPropertyQRY { 
                    Id = cp.Id,
                    PropertyName = cp.PropertyName, 
                    PropertyValue = cp.PropertyValue
                }).ToList()
            }).ToListAsync();

            return result;
        }

        public async Task<DataTable> GetChartDataByQueryAsync(string query)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable result = new DataTable();
                            adapter.Fill(result);
                            return result;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    return null;
                }
            }

        }

        public async Task UpdateChartAsync(int id, string label, string query, List<ChartPropertyDTO> chartPropertiesDto)
        {
            var chart = await _context.Charts.FindAsync(id);
            chart.Label = label;
            chart.Query = query;
        
            var existingProperties = await _context.ChartProperties
                .Where(cp => cp.ChartId == id)
                .ToListAsync();

            _context.ChartProperties.RemoveRange(existingProperties);

            foreach (var chartpropertydto in chartPropertiesDto)
            {
                var chartProperty = new ChartProperty
                {
                    ChartId = chart.Id,
                    PropertyName = chartpropertydto.PropertyName,
                    PropertyValue = chartpropertydto.PropertyValue
                };

                await _context.ChartProperties.AddAsync(chartProperty);
            }

        }
    }
}
