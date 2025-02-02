using System.Collections.Generic;
using System.Linq;
using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Areas.Shop.DTOS;
using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Enum;
using ecommerce_dash_api.Models;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.MachineLearning;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace ecommerce_dash_api.Areas.Shop.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly EcommerceContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ISetupRepository _setupRepository;
        private readonly ElasticsearchClient _elasticsearchClient;
        private readonly string _elasticSearchIndex;
        public ElasticSearchService(EcommerceContext context, ElasticsearchClient elasticsearchClient, IConfiguration configuration, IProductRepository productRepository, ISetupRepository setupRepository)
        {
            _context = context;
            _elasticsearchClient = elasticsearchClient;
            _elasticSearchIndex = configuration["ElasticSettings:DefaultIndex"];
            _productRepository = productRepository;
            _setupRepository = setupRepository;
        }

        public async Task IndexProductAsync(ShopProductQRY product)
        {
            var response = await _elasticsearchClient.IndexAsync(product, idx => idx.Index(_elasticSearchIndex));
            if (!response.IsValidResponse)
            {
                throw new Exception("Product indexing failed");
            }
        }
        public async Task UpdateIndexProductAsync(int productId, int transactionValue)
        {
            var shopProduct = await _productRepository.GetShopProductByProductId(productId);
            if (shopProduct == null)
            {
                throw new InvalidOperationException("Please add a product content");
            }
            shopProduct.TotalQuantity = transactionValue;
            var response = await _elasticsearchClient.IndexAsync(shopProduct, idx => idx.Index(_elasticSearchIndex));
            if (!response.IsValidResponse)
            {
                throw new InvalidOperationException("Product indexing failed");
            }
        }
        public async Task UpdateIndexProductAsync(Product product)
        {
            var shopProduct = await _productRepository.GetShopProductByProductId(product.Id);
            if (shopProduct == null)
            {
                return;
            }
            shopProduct.Name = product.Name;
            shopProduct.Price = product.Price;
            shopProduct.Discount = product.Discount;
            var currentBrand = await _setupRepository.GetBrandByIdAsync(product.BrandId);
            shopProduct.Brand = currentBrand?.Name;
            var currentSeason = await _setupRepository.GetSeasonByIdAsync(product.SeasonId);
            shopProduct.Season = currentSeason?.Name;
            shopProduct.Year = product.Year;
            shopProduct.IsActive = product.IsActive;
            var response = await _elasticsearchClient.IndexAsync(shopProduct, idx => idx.Index(_elasticSearchIndex));
            if (!response.IsValidResponse)
            {
                throw new InvalidOperationException("Product indexing failed");
            }
        }
        public async Task UpdateIndexProductAsync(ProductInfo productInfo, List<ProductCategory> productCategories)
        {
            var shopProduct = await _productRepository.GetShopProductByProductId(productInfo.ProductId);
            if (shopProduct == null)
            {
                return;
            }
            shopProduct.ShortDescription = productInfo.ShortDescription;
            shopProduct.LongDescription = productInfo.LongDescription;
            shopProduct.Weight = productInfo.Weight;
            shopProduct.ShippingWeight = productInfo.ShippingWeight;
            shopProduct.MinOrder = productInfo.MinOrder;
            shopProduct.MaxOrder = productInfo.MaxOrder;
            //shopProduct.TotalQuantity = await _productRepository.GetTotalQuantityByProductIdAsync(productInfo.ProductId);
            var categoryTasks = productCategories?.Select(async productCategory =>
            {
                using (var _context2 = new EcommerceContext())
                {
                    return await _context2.Categories
                        .Where(c => c.Id == productCategory.ProductId)
                        .Select(c => c.Name)
                        .FirstOrDefaultAsync();
                }
            });

            var categoryNames = await Task.WhenAll(categoryTasks);
            shopProduct.Categories.AddRange(categoryNames);
            var response = await _elasticsearchClient.IndexAsync(shopProduct, idx => idx.Index(_elasticSearchIndex));
            if (!response.IsValidResponse)
            {
                throw new InvalidOperationException("Product indexing failed");
            }
        }
        public async Task<long> GetCountSearchProductsAsync(SearchQueryDTO searchQuery)
        {
            var shouldClauses = new List<Action<QueryDescriptor<ShopProductQRY>>>();
            var minimumShouldMatch = 1;

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Name)
                .Query(searchQuery.Query)
                .Operator(Operator.And)
                .Boost(10)
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Brand)
                .Query(searchQuery.Query)
                .Boost(5)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Categories)
                .Query(searchQuery.Query)
                .Boost(4)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.ShortDescription)
                .Query(searchQuery.Query)
                .Boost(3)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.LongDescription)
                .Query(searchQuery.Query)
                .Boost(2)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Season)
                .Query(searchQuery.Query)
                .Boost(1)
            ));

            if (searchQuery?.Brands.Count > 0) minimumShouldMatch++;
            foreach (var brand in searchQuery?.Brands)
            {
                shouldClauses.Add(sh => sh.Match(m => m
                    .Field(f => f.Brand)
                    .Query(brand)
                ));
            }

            if (searchQuery?.Categories.Count > 0) minimumShouldMatch++;
            foreach (var category in searchQuery?.Categories)
            {
                shouldClauses.Add(sh => sh.Match(m => m
                    .Field(f => f.Categories)
                    .Query(category)
                ));
            }
            var countResponse  = await _elasticsearchClient.CountAsync<ShopProductQRY>(s => s
                .Indices(_elasticSearchIndex)
                .Query(q => q
                    .Bool(b => b
                        .Should(shouldClauses.ToArray())
                        .MinimumShouldMatch(minimumShouldMatch)
                        .Filter(f => f
                              .Bool(bb => bb
                                     .Must(
                                          fm => fm.Range(r => r
                                                .NumberRange(nr => nr
                                                    .Field(fld => fld.TotalQuantity)
                                                    .Gt(0)
                                                )
                                          )
                                     )
                              )
                        )
                    )
                )
            );
      
            return countResponse?.Count ?? 0;
        }
        public async Task<FilterSortQRY> GetUniqueValuesAsync(SearchQueryDTO searchQuery)
        {
            var result = new FilterSortQRY();
            var minimumShouldMatch = 1;
            var shouldClauses = new List<Action<QueryDescriptor<ShopProductQRY>>>();

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Name)
                .Query(searchQuery.Query)
                .Operator(Operator.And)
                .Boost(10)
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Brand)
                .Query(searchQuery.Query)
                .Boost(5)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Categories)
                .Query(searchQuery.Query)
                .Boost(4)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.ShortDescription)
                .Query(searchQuery.Query)
                .Boost(3)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.LongDescription)
                .Query(searchQuery.Query)
                .Boost(2)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Season)
                .Query(searchQuery.Query)
                .Boost(1)
            ));

            var response = await _elasticsearchClient.SearchAsync<ShopProductQRY>(s => s
                .Index(_elasticSearchIndex)
                .Query(q => q
                    .Bool(b => b
                        .Should(shouldClauses.ToArray())
                        .MinimumShouldMatch(minimumShouldMatch)
                        .Filter(f => f
                              .Bool(bb => bb
                                     .Must(
                                          fm => fm.Range(r => r
                                                .NumberRange(nr => nr
                                                    .Field(fld => fld.TotalQuantity)
                                                    .Gt(0)
                                                )
                                          )
                                     )
                              )
                        )
                    )
                )        
            );

            if (response.IsValidResponse)
            {
                var distinctBrandsTask = Task.Run(() => response?.Hits
                .Select(hit => hit?.Source)
                .Select(i => i?.Brand)
                .Distinct()
                .ToList());

                var distinctCategoriesTask = Task.Run(() => response?.Hits
                    .Select(hit => hit?.Source)
                    .SelectMany(i => i?.Categories)
                    .Distinct()
                    .ToList());

                var distinctBrands = await distinctBrandsTask;
                var distinctCategories = await distinctCategoriesTask;

                result.Brands = distinctBrands;
                result.Categories = distinctCategories;
            }
            return result;
        }
        public async Task<List<ShopProductQRY>> SearchProductsAsync(SearchQueryDTO searchQuery)
        {
            var shouldClauses = new List<Action<QueryDescriptor<ShopProductQRY>>>();
            var sortOptions = new List<SortOptions>();
            var minimumShouldMatch = 1;
            int from = (searchQuery.PageNbr - 1) * searchQuery.PageSize;

            if (!string.IsNullOrEmpty(searchQuery?.SortingOption))
            {
                if(searchQuery?.SortingOption == SortingOptionsEnum.PriceLowToHigh.ToString())
                {
                    var fieldSort = new FieldSort
                    {
                        Order = SortOrder.Asc
                    };
                    sortOptions.Add(SortOptions.Field(Infer.Field<ShopProductQRY>(f => f.Price), fieldSort));
                }
                else if(searchQuery?.SortingOption == SortingOptionsEnum.PriceHighToLow.ToString())
                {
                    var fieldSort = new FieldSort
                    {
                        Order = SortOrder.Desc
                    };
                    sortOptions.Add(SortOptions.Field(Infer.Field<ShopProductQRY>(f => f.Price), fieldSort));
                }          
            }

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Name)
                .Query(searchQuery.Query)
                .Operator(Operator.And)
                .Boost(10)
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Brand)
                .Query(searchQuery.Query)
                .Boost(5)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Categories)
                .Query(searchQuery.Query)
                .Boost(4)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.ShortDescription)
                .Query(searchQuery.Query)
                .Boost(3)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.LongDescription)
                .Query(searchQuery.Query)
                .Boost(2)
                .Fuzziness(new Fuzziness("AUTO"))
            ));

            shouldClauses.Add(sh => sh.Match(m => m
                .Field(f => f.Season)
                .Query(searchQuery.Query)
                .Boost(1)
            ));

            if (searchQuery?.Brands.Count > 0) minimumShouldMatch++;
            foreach (var brand in searchQuery?.Brands)
            {
                shouldClauses.Add(sh => sh.Match(m => m
                    .Field(f => f.Brand)
                    .Query(brand)
                ));
            }

            if (searchQuery?.Categories.Count > 0) minimumShouldMatch++;
            foreach (var category in searchQuery?.Categories)
            {
                shouldClauses.Add(sh => sh.Match(m => m
                    .Field(f => f.Categories)
                    .Query(category)
                ));
            }

            var searchResponse = await _elasticsearchClient.SearchAsync<ShopProductQRY>(s => s
                .Index(_elasticSearchIndex)
                .From(from)
                .Size(searchQuery.PageSize)
                .Query(q => q
                    .Bool(b => b
                        .Should(shouldClauses.ToArray())
                        .MinimumShouldMatch(minimumShouldMatch)
                        .Filter(f => f
                              .Bool(bb => bb
                                     .Must(
                                          fm => fm.Range(r => r
                                                .NumberRange(nr => nr
                                                    .Field(fld => fld.TotalQuantity)
                                                    .Gt(0)
                                                )
                                          )
                                     )
                              )
                        )
                    )     
                )
                .Sort(sortOptions)
            );


            if (!searchResponse.IsValidResponse)
            {
                return null;
            }

            var result = searchResponse?.Documents?.ToList();
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





//public async Task<List<ShopProductQRY>> SearchProductsAsync(SearchQueryDTO searchQuery)
//{
//    int from = (searchQuery.PageNbr - 1) * searchQuery.PageSize;

//    var searchResponse = await _elasticsearchClient.SearchAsync<ShopProductQRY>(s => s
//        .Index(_elasticSearchIndex)
//        .From(from)
//        .Size(searchQuery.PageSize)
//        .Query(q => q
//            .QueryString(qs => qs.Query(searchQuery.Query))
//            .Bool(b => b
//                .Should(
//                    s => s.Match(m => m.Field(f => f.Categories).Query("KIDS")),
//                    s => s.Match(m => m.Field(f => f.Categories).Query("MEN"))
//                )
//                .MinimumShouldMatch(1)
//            )
//        )
//        .Sort(s => s
//            .Field(Infer.Field<ShopProductQRY>(f => f.Id), fs => fs.Order(SortOrder.Desc))
//        )
//    );

//    var result = searchResponse?.Documents?.ToList();
//    return result;
//}


//.Must(m => m
//    .QueryString(qs => qs
//        .Query(searchQuery.Query)
//        .Fuzziness(new Fuzziness("AUTO"))
//    )
//)

//.Should(shouldClauses.ToArray())