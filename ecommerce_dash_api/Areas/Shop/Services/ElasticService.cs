using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Models;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using Microsoft.EntityFrameworkCore;


namespace ecommerce_dash_api.Areas.Shop.Services
{
    public class ElasticService : IElasticService
    {
        private readonly EcommerceContext _context;
        private readonly ElasticsearchClient _elasticsearchClient;
        private readonly string _elasticSearchIndex;
        public ElasticService(EcommerceContext context, ElasticsearchClient elasticsearchClient, IConfiguration configuration)
        {
            _context = context;
            _elasticsearchClient = elasticsearchClient;
            _elasticSearchIndex = configuration["ElasticSettings:DefaultIndex"];
        }

        public async Task<long> GetCountSearchProductsAsync(string query)
        {
            var countResponse = await _elasticsearchClient.CountAsync<ShopProductQRY>(c => c
            .Indices(_elasticSearchIndex)
                .Query(q => q
                    .QueryString(qs => qs
                        .Query(query)
                    )
                )
            );
            return countResponse?.Count ?? 0;
        }

        public async Task IndexProductAsync(ShopProductQRY product)
        {
            var response = await _elasticsearchClient.IndexAsync(product, idx => idx.Index(_elasticSearchIndex));
            if (!response.IsValidResponse)
            {
                throw new Exception($"Failed to index product.");
            }
        }

        public async Task<List<ShopProductQRY>> SearchProductsAsync(string query, int pageNbr, int pageSize)
        {
            int from = (pageNbr - 1) * pageSize;

            var searchResponse = await _elasticsearchClient.SearchAsync<ShopProductQRY>(s => s
                .Index(_elasticSearchIndex)
                .From(from)                 
                .Size(pageSize)
                .Query(q => q
                    .QueryString(qs => qs
                        .Query(query) 
                    )
                )
                .Sort(
                s => s.Field(Infer.Field<ShopProductQRY>(f => f.Id), fs => fs.Order(SortOrder.Desc)))
            );

            var result = searchResponse.Documents.ToList();
            return result;
        }

        public async Task UpdateElasticDatabaseAsync()
        {
            // Fetch all products from the database
            var products = await (
                from p in _context.Products
                join pi in _context.ProductInfos on p.Id equals pi.ProductId
                join pm in _context.ProductMedia on p.Id equals pm.ProductId into mediaGroup
                join supplier in _context.Suppliers on p.SupplierId equals supplier.Id
                join brand in _context.Brands on p.BrandId equals brand.Id
                join season in _context.Seasons on p.SeasonId equals season.Id
                select new ShopProductQRY
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Discount = p.Discount,
                    Brand = brand.Name ?? "",
                    Season = season.Name ?? "",
                    Year = p.Year,
                    LongDescription = pi.LongDescription,
                    ShortDescription = pi.ShortDescription,
                    Weight = pi.Weight,
                    ShippingWeight = pi.ShippingWeight,
                    MinOrder = pi.MinOrder,
                    MaxOrder = pi.MaxOrder,
                    MediaUrl = mediaGroup.OrderBy(m => m.Id).Select(m => m.Url).FirstOrDefault() ?? "",
                    ProductCategoryIds = _context.ProductCategories
                            .Where(c => c.ProductId == p.Id)
                            .Select(c => c.CategoryId).ToList(),
                    Categories = new List<string>(),
                }).ToListAsync();

            // Create a list of tasks to process products concurrently
            var productTasks = products.Select(async product =>
            {
                using(var _context2 = new EcommerceContext())
                {
                    // Calculate the total quantity
                    var totalQuantity = await _context2.ProductQuantities
                        .Where(pq => pq.ProductId == product.Id)
                        .SumAsync(pq => pq.Quantity);

                    product.TotalQuantity = totalQuantity;

                    // Fetch category names
                    var categoryTasks = product.ProductCategoryIds?.Select(async categoryId =>
                    {
                        using (var _context3 = new EcommerceContext())
                        {
                            return await _context3.Categories
                                .Where(c => c.Id == categoryId)
                                .Select(c => c.Name)
                                .FirstOrDefaultAsync();
                        }
                    });

                    if (categoryTasks != null)
                    {
                        var categoryNames = await Task.WhenAll(categoryTasks);
                        product.Categories.AddRange(categoryNames.Where(name => name != null));
                    }
                    var response = await _elasticsearchClient.IndexAsync(product, idx => idx.Index(_elasticSearchIndex));
                }
            });

            // Wait for all product tasks to complete
            await Task.WhenAll(productTasks);
        }
    }
}
