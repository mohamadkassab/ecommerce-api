using System.Collections.Immutable;
using ecommerce_dash_api.Areas.Dashboard.QRYS;
using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_dash_api.Areas.Shop.Repositories
{
    public class ShopProductRepository : IShopProductRepository
    {
        private readonly EcommerceContext _context;
        public ShopProductRepository(EcommerceContext context) 
        {
            _context = context;
        }
        public async Task<List<CategoryProductQRY>> GetProductsByCategoryAndSize(int categoryId = 0, int page = 1, int pageSize = 12)
        {
            var result = new List<CategoryProductQRY>();    
            var productCategories = await _context.ProductCategories
                .Where(pc => categoryId == 0 || pc.CategoryId == categoryId)
                .GroupBy(pc => pc.CategoryId)
                .Select(g => new
                {
                    CatetegoryId = g.Key,
                    ProductIds = g.OrderByDescending(pc => pc.Id)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .Select(pc => pc.ProductId)
                                .ToList()
                }).ToListAsync();

            var getProductTasks = productCategories.Select(async productCategory =>
            {

                var catName = _context.Categories
                              .Where(c => c.Id == productCategory.CatetegoryId)
                              .FirstOrDefault()?.Name;

                var products = await (from p in _context.Products
                                      join pi in _context.ProductInfos on p.Id equals pi.ProductId
                                      join pm in _context.ProductMedia on p.Id equals pm.ProductId into mediaGroup
                                      join supplier in _context.Suppliers on p.SupplierId equals supplier.Id
                                      join brand in _context.Suppliers on p.BrandId equals brand.Id
                                      join season in _context.Suppliers on p.SeasonId equals season.Id
                                      where productCategory.ProductIds.Contains(p.Id)
                                      select new ShopProductQRY
                                      {
                                          Id = p.Id,
                                          Name = p.Name,
                                          Cost = p.Cost,
                                          Price = p.Price,
                                          Discount = p.Discount,
                                          Note = p.Note ?? "",
                                          Supplier = supplier.Name ?? "",
                                          Brand = brand.Name ?? "",
                                          Season = season.Name ?? "",
                                          Year = p.Year,
                                          LongDescription = pi.LongDescription,
                                          ShortDescription = pi.ShortDescription,
                                          Weight = pi.Weight,
                                          ShippingWeight = pi.ShippingWeight,
                                          MinOrder = pi.MinOrder,
                                          MaxOrder = pi.MaxOrder,
                                          MediaUrl = mediaGroup.OrderBy(m => m.Id).Select(m => m.Url).FirstOrDefault(),
                                      }).Distinct().ToListAsync();

                if(categoryId == 0)
                {
                    var getImageTasks = products.Select(async product =>
                    {
                        product.Media = await Helpers.GetFileByUrlAsync(product.MediaUrl);
                        product.MediaUrl = null;
                    }).ToArray();

                    await Task.WhenAll(getImageTasks);
                }

                result.Add(new CategoryProductQRY
                {
                    CategoryId = productCategory.CatetegoryId,
                    CategoryName = catName,
                    Products = products,
                });
            });

            Task.WhenAll(getProductTasks);
            return result;
        }
    }
}
