using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_dash_api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceContext _context;
        private readonly IHelpersFunctions _helpersFunctions;
        public ProductRepository(EcommerceContext context, IHelpersFunctions helpersFunctions)
        {
            _context = context;
            _helpersFunctions = helpersFunctions;
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
            .Select(i => new ProductQRY
            {
                Id = i.Id,
                Code = i.Code,
                Name = i.Name,
                Cost = i.Cost,
                Price = i.Price,
                Discount = i.Discount,
                Note = i.Note ?? "",
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
        public  Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Product Content                                           
        //+------------------------------------------------------------------+
        public async Task<List<ProductContentQRY>> GetAllProductContentsAsync()
        {
            var result = await _context.ProductInfos
                .AsNoTracking()
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
                    Tags = _context.ProductTags
                    .Where(pt => pt.ProductId == i.ProductId)
                    .Select(pt => pt.Tag.Name)
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
        //| Product Tag                                           
        //+------------------------------------------------------------------+
        public async Task<ProductTag?> GetProductTagByIdAsync(int id)
        {
            return await _context.ProductTags.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task CreateProductTagAsync(ProductTag productTag)
        {
            await _context.ProductTags.AddAsync(productTag);
        }
        public async Task CreateProductTagRangeAsync(List<ProductTag> productTagRange)
        {
            await _context.ProductTags.AddRangeAsync(productTagRange);
        }
        public Task UpdateProductTagAsync(ProductTag productTag)
        {
            _context.ProductTags.Update(productTag);
            return Task.CompletedTask;
        }
        public Task DeleteProductTagsByProductIdAsync(int productId)
        {
            var productTags = _context.ProductTags
                         .Where(i => i.ProductId == productId);

            if (productTags.Any())
            {
                _context.ProductTags.RemoveRange(productTags);
            }
            return Task.CompletedTask;
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
    }
}
