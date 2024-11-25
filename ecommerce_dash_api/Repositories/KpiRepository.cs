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

        //+------------------------------------------------------------------+
        //| Kpi                                            
        //+------------------------------------------------------------------+
        public Task<List<ChartQRY>> GetAllChartsAsync()
        {
            var result = _context.Charts.Select(i => new ChartQRY
            {
                Id = i.Id,
                Label = i.Label,
                Query = i.Query,
                ChartType = i.Type,
                ChartProperties = i.ChartProperties.Select(cp => new ChartPropertyQRY
                {
                    Id = cp.Id,
                    PropertyName = cp.Name,
                    PropertyValue = cp.Value
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
        public async Task CreateChartAsync(ChartProperty charproperty)
        {
            await _context.ChartProperties.AddAsync(charproperty);
        }
        public async Task UpdateChartAsync(int id, string label, string query, List<ChartPropertyDTO> chartPropertiesDto, string username)
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
                    Name = chartpropertydto.PropertyName,
                    Value = chartpropertydto.PropertyValue,
                    UpdatedBy = username
                };

                await _context.ChartProperties.AddAsync(chartProperty);
            }
        }
        public async Task DeleteChartAsync(Chart? chart)
        {
            _context.Charts.Remove(chart);
        }
    }
}
