using System.Collections.Immutable;
using ecommerce_dash_api.Areas.Dashboard.QRYS;
using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ecommerce_dash_api.Areas.Shop.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly EcommerceContext _context;
        public PageRepository(EcommerceContext context) 
        {
            _context = context;
        }

        public async Task<List<ShopBrandQRY>> GetAllBrandsAsync()
        {
            using (var _context2 = new EcommerceContext()) 
            {
                var result = new List<ShopBrandQRY>();
                var brands = await _context2.Brands.ToListAsync();
                var task = brands.Select(async brand =>
                {
                    result.Add(new ShopBrandQRY
                    {
                        Name = brand.Name,
                        Media = await Helpers.GetFileByUrlAsync(brand.LogoUrl),
                    });
                });
                await Task.WhenAll(task);
                return result;
            }
        }

        public async Task<List<CategoryProductQRY>> GetProductsBySizeAsync(int pagenNbr, int pageSize)
        {
            var result = new List<CategoryProductQRY>();    

            var productCategories = await _context.ProductCategories
                .GroupBy(pc => pc.CategoryId)
                .Select(g => new
                {
                    CategoryId = g.Key,
                    ProductIds = g.OrderByDescending(pc => pc.ProductId)
                                .Where(pc => _context.ProductQuantities.Where(pq => pq.ProductId == pc.ProductId && pq.Quantity > 0).Any())
                                .Skip((pagenNbr - 1) * pageSize)
                                .Take(pageSize)
                                .Select(pc => pc.ProductId)
                                .ToList()
                }).ToListAsync();

            var getProductTasks = productCategories.Select(async productCategory =>
            {
                if(productCategory.ProductIds.Count > 0) 
                {
                    using(var _context2 = new EcommerceContext())
                    using(var _context3 = new EcommerceContext())
                    {
                        var catName = await _context3.Categories
                            .Where(c => c.Id == productCategory.CategoryId)
                            .Select(c => c.Name)
                            .FirstOrDefaultAsync();

                        var products = await (
                            from p in _context2.Products
                            join pi in _context2.ProductInfos on p.Id equals pi.ProductId
                            join pm in _context2.ProductMedia on p.Id equals pm.ProductId into mediaGroup
                            join supplier in _context2.Suppliers on p.SupplierId equals supplier.Id
                            join brand in _context2.Brands on p.BrandId equals brand.Id
                            join season in _context2.Seasons on p.SeasonId equals season.Id
                            where productCategory.ProductIds.Contains(p.Id) 
                            && p.IsActive 
                                      
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
                        }).Distinct().ToListAsync();

                        var getImageTasks =  products.Select(async product =>
                        {
                            product.Media = await Helpers.GetFileByUrlAsync(product.MediaUrl);
                            product.MediaUrl = null;
                        }).ToArray();

                        await Task.WhenAll(getImageTasks);


                        result.Add(new CategoryProductQRY
                        {
                            CategoryId = productCategory.CategoryId,
                            CategoryName = catName ?? "",
                            Products = products,
                        });
                    }
                }
            });

            await Task.WhenAll(getProductTasks);
            return result;
        } 

        public async Task<int> GetTotalProductsByCategoryAsync(int categoryId)
        {
            using (var _context2 = new EcommerceContext())
            {
                var result = await _context2.ProductCategories
                    .Where(pc => pc.CategoryId == categoryId)
                    .Where(pc => _context2.ProductQuantities.Any(pq => pq.ProductId == pc.ProductId && pq.Quantity > 0))
                    .CountAsync();

                return result;
            }
        }
    }
}







//public async Task<List<ShopProductQRY>> GetProductsByCategoryAndSizeAsync(int categoryId, int pagenNbr, int pageSize)
//{
//    var result = new List<ShopProductQRY>();

//    var productIds = await _context.ProductCategories
//        .Where(pc => pc.CategoryId == categoryId)
//        .Where(pc => _context.ProductQuantities.Any(pq => pq.ProductId == pc.ProductId && pq.Quantity > 0))
//        .OrderByDescending(pc => pc.ProductId)
//        .Skip((pagenNbr - 1) * pageSize)
//        .Take(pageSize)
//        .Select(pc => pc.ProductId)
//        .ToListAsync();

//    var getProductTasks = productIds.Select(async productId =>
//    {
//        using (var _context2 = new EcommerceContext())
//        {
//            var current_product = await (
//            from p in _context2.Products
//            join pi in _context2.ProductInfos on p.Id equals pi.ProductId
//            join pm in _context2.ProductMedia on p.Id equals pm.ProductId into mediaGroup
//            join supplier in _context2.Suppliers on p.SupplierId equals supplier.Id
//            join brand in _context2.Brands on p.BrandId equals brand.Id
//            join season in _context2.Seasons on p.SeasonId equals season.Id
//            where p.Id == productId && p.IsActive

//            select new ShopProductQRY
//            {
//                Id = p.Id,
//                Name = p.Name,
//                Price = p.Price,
//                Discount = p.Discount,
//                Brand = brand.Name ?? "",
//                Season = season.Name ?? "",
//                Year = p.Year,
//                LongDescription = pi.LongDescription,
//                ShortDescription = pi.ShortDescription,
//                Weight = pi.Weight,
//                ShippingWeight = pi.ShippingWeight,
//                MinOrder = pi.MinOrder,
//                MaxOrder = pi.MaxOrder,
//                MediaUrl = mediaGroup.OrderBy(m => m.Id).Select(m => m.Url).FirstOrDefault() ?? "",
//                ProductCategoryIds = _context2.ProductCategories
//                        .Where(c => c.ProductId == p.Id)
//                        .Select(c => c.CategoryId).ToList(),
//                Categories = new List<string>(),
//                IsActive = p.IsActive,
//            }).FirstOrDefaultAsync();

//            var categoryTasks = current_product.ProductCategoryIds.Select(async categoryId =>
//            {
//                using (var _context3 = new EcommerceContext())
//                {
//                    return await _context3.Categories
//                          .Where(c => c.Id == categoryId)
//                          .Select(c => c.Name)
//                          .FirstOrDefaultAsync();
//                }
//            });

//            var categoryNames = await Task.WhenAll(categoryTasks);
//            current_product.Categories.AddRange(categoryNames.Where(name => name != null));

//            current_product.Media = await Helpers.GetFileByUrlAsync(current_product.MediaUrl);
//            current_product.MediaUrl = null;
//            result.Add(current_product);
//        }
//    });

//    await Task.WhenAll(getProductTasks);
//    return result;
//}