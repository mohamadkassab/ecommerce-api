using ecommerce_dash_api.Areas.Dashboard.DTOS;
using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Areas.Dashboard.QRYS;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_dash_api.Areas.Dashboard.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceContext _context;
        public ProductRepository(EcommerceContext context)
        {
            _context = context;
        }

        //+------------------------------------------------------------------+
        //| Product                                            
        //+------------------------------------------------------------------+
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<List<ProductQRY>> GetAllProductsAsync()
        {
            var result = await _context.Products
            .AsNoTracking()
            .OrderBy(i => i.Id)
            .Select(i => new ProductQRY
            {
                Id = i.Id,
                Code = i.Code,
                Name = i.Name,
                Cost = i.Cost,
                Price = i.Price,
                Discount = i.Discount,
                Note = i.Note ?? "",
                attributes = _context.ProductAttributes.Where(pa => pa.ProductId == i.Id).Select(pa => pa.Attribute).ToList(),
                Supplier = i.Supplier.Name,
                Brand = i.Brand.Name,
                Year = i.Year,
                Season = i.Season.Name,
                IsActive = i.IsActive,
                UpdatedAt = i.UpdatedAt,
                UpdatedBy = i.UpdatedBy ?? "",
            })
            .ToListAsync();

            return result;
        }
        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }
        public async Task CreateProductAttributeRangeAsync(List<ProductAttribute> productAttributes)
        {
            await _context.ProductAttributes.AddRangeAsync(productAttributes);
        }
        public Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            return Task.CompletedTask;
        }
        public Task DeleteProductAttributeByProductIdAsync(int productId)
        {
            var productAttributes = _context.ProductAttributes
                         .Where(i => i.ProductId == productId);

            if (productAttributes.Any())
            {
                _context.ProductAttributes.RemoveRange(productAttributes);
            }
            return Task.CompletedTask;
        }


        //+------------------------------------------------------------------+
        //| Product Attribute                                           
        //+------------------------------------------------------------------+
        public async Task<List<string>> GetProductAttributesByProductIdAsync(int productId)
        {
            using (var _contex2 = new EcommerceContext()) 
            {
                var result = await _contex2.ProductAttributes
                  .Where(i => i.ProductId == productId)
                  .Select(i => i.Attribute)
                  .ToListAsync();

                return result;
            }
        }

        //+------------------------------------------------------------------+
        //| Product Content                                           
        //+------------------------------------------------------------------+
        public async Task<List<ProductContentQRY>> GetAllProductContentsAsync()
        {
            var result = await _context.ProductInfos
                .AsNoTracking()
                .OrderBy(i => i.ProductId)
                .Select(i => new ProductContentQRY
                {
                    ProductId = i.ProductId,
                    ShortDescription = i.ShortDescription,
                    LongDescription = i.LongDescription,
                    Weight = i.Weight,
                    ShippingWeight = i.ShippingWeight,
                    MinOrder = i.MinOrder,
                    MaxOrder = i.MaxOrder,
                    Categories = _context.ProductCategories
                    .Where(pc => pc.ProductId == i.ProductId)
                    .Select(pc => pc.Category.Name)
                    .ToList(),
                    MediaUrls = _context.ProductMedia
                    .Where(pm => pm.ProductId == i.ProductId)
                    .Select(pm => pm.Url)
                    .ToList(),
                    UpdatedAt = i.UpdatedAt,
                    UpdatedBy = i.UpdatedBy ?? "",
                })
                .ToListAsync();

            return result;
        }

        //+------------------------------------------------------------------+
        //| Product Category                                           
        //+------------------------------------------------------------------+
        public async Task<ProductCategory?> GetProductCategoryByIdAsync(int id)
        {
            return await _context.ProductCategories.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task CreateProductCategoryAsync(ProductCategory productCategory)
        {
            await _context.ProductCategories.AddAsync(productCategory);
        }
        public async Task CreateProductCategoryRangeAsync(List<ProductCategory> productCategoryRange)
        {
            await _context.ProductCategories.AddRangeAsync(productCategoryRange);
        }
        public Task UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
            return Task.CompletedTask;
        }
        public Task DeleteProductCategoriesByProductIdAsync(int productId)
        {
            var productCategories = _context.ProductCategories
                                     .Where(i => i.ProductId == productId);

            if (productCategories.Any())
            {
                _context.ProductCategories.RemoveRange(productCategories);
            }
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Product Info                                           
        //+------------------------------------------------------------------+
        public async Task<ProductInfo?> GetProductInfoByProductIdAsync(int productId)
        {
            return await _context.ProductInfos.FirstOrDefaultAsync(i => i.ProductId == productId);
        }
        public async Task CreateProductInfoAsync(ProductInfo productInfo)
        {
            await _context.ProductInfos.AddAsync(productInfo);
        }
        public async Task CreateProductInfoRangeAsync(List<ProductInfo> productInfoRange)
        {
            await _context.ProductInfos.AddRangeAsync(productInfoRange);
        }
        public Task UpdateProductInfoAsync(ProductInfo productInfo)
        {
            _context.ProductInfos.Update(productInfo);
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Product Media                                           
        //+------------------------------------------------------------------+
        public async Task<List<string>?> GetProductMediaUrlsByProductIdAsync(int productId)
        {
            return await _context.ProductMedia
                       .Where(i => i.ProductId == productId)
                       .Select(i => i.Url)
                       .ToListAsync();
        }
        public async Task CreateProductMediaAsync(ProductMedium productMedia)
        {
            await _context.ProductMedia.AddAsync(productMedia);
        }
        public async Task CreateProductMediaRangeAsync(IEnumerable<ProductMedium> productMediaRange)
        {
            await _context.ProductMedia.AddRangeAsync(productMediaRange);
        }
        public Task UpdateProductMediaAsync(ProductMedium productMedia)
        {
            _context.ProductMedia.Update(productMedia);
            return Task.CompletedTask;
        }
        public Task DeleteProductMediaByProductIdAsync(int productId)
        {
            var productMedia = _context.ProductMedia.Where(i => i.ProductId == productId);

            if (productMedia.Any())
            {
                _context.ProductMedia.RemoveRange(productMedia);
            }
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Product Quantity                                         
        //+------------------------------------------------------------------+
        public async Task<List<GroupProductQuantityAttributes>> GetAllProductQuantityByProductIdAsync(int productId)
        {
            using(var _context2 = new EcommerceContext())
            {
                var result = 
                await (from pq in _context2.ProductQuantities
                        where pq.ProductId == productId
                        join pqa in _context2.ProductQuantityAttributes on pq.Id equals pqa.ProductQuantityId
                        group pqa by pq.Id into groupedPqa
                        select new GroupProductQuantityAttributes
                        {
                            ProductQuantityId = groupedPqa.Key,
                            productQuantityAttrbiutes = groupedPqa.ToList()
                        }).ToListAsync();
                return result;
            }
        }
        public async Task<int> GetTotalQuantityByProductIdAsync (int productId)
        {
            var result = await _context.ProductQuantities.Where(pq => pq.ProductId == productId).SumAsync(pq => pq.Quantity);
            return result ?? 0;
        }
        public async Task CreateProductQuantityAsync(ProductQuantity productQuantity)
        {
           await _context.ProductQuantities.AddAsync(productQuantity);
        }
        public Task UpdateProductQuantityAsync(ProductQuantity productQuantity)
        {
            _context.ProductQuantities.Update(productQuantity);
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Shop Product                                            
        //+------------------------------------------------------------------+
        public async Task<ShopProductQRY> GetShopProductByProductId(int productId)
        {
            using(var _context2 = new EcommerceContext())
            {
                var result =
                   await (
                       from p in _context2.Products
                       join pi in _context2.ProductInfos on p.Id equals pi.ProductId
                       join pm in _context2.ProductMedia on p.Id equals pm.ProductId into mediaGroup
                       join supplier in _context2.Suppliers on p.SupplierId equals supplier.Id
                       join brand in _context2.Brands on p.BrandId equals brand.Id
                       join season in _context2.Seasons on p.SeasonId equals season.Id
                       where p.Id == productId

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
                           ProductCategoryIds = _context2.ProductCategories
                                   .Where(c => c.ProductId == p.Id)
                                   .Select(c => c.CategoryId).ToList(),
                           Categories = new List<string>(),
                       }).FirstOrDefaultAsync();
                return result;
            }
           
        }

        //+------------------------------------------------------------------+
        //| Transaction                                         
        //+------------------------------------------------------------------+
        public async Task<List<TransactionQRY>> GetAllTransactionsAsync()
        {
            var result = await _context.Transactions
                .AsNoTracking()
                .Select(i => new TransactionQRY
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    TransactionType = i.TransactionType,
                    Note = i.Note,
                    UpdatedAt = i.UpdatedAt,
                    UpdatedBy = i.UpdatedBy ?? "",
                    transactionAttributes = i.TransactionAttributes.Select(ta => new TransactionAttributeDTO
                    {
                        Name = ta.Attribute,
                        Option = ta.AttributeOption
                    }).ToList(),
                })
                .ToListAsync();

            return result;
        }
        public async Task CreateTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }
        public async Task CreateTransactionAttributesAsync(List<TransactionAttribute> transactionAttributes)
        {
            await _context.TransactionAttributes.AddRangeAsync(transactionAttributes);
        }

    }
}
