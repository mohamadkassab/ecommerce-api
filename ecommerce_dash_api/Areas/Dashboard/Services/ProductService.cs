using ecommerce_dash_api.Areas.Dashboard.DTOS;
using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Areas.Dashboard.QRYS;
using ecommerce_dash_api.Enum;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_dash_api.Areas.Dashboard.Services
{
    public class ProductService : IProductService
    {
        private readonly EcommerceContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ISetupRepository _setupRepository;

        public ProductService(IProductRepository productRepository, ISetupRepository setupRepository, EcommerceContext context)
        {
            _productRepository = productRepository;
            _setupRepository = setupRepository;
            _context = context;
        }

        //+------------------------------------------------------------------+
        //| Product                                            
        //+------------------------------------------------------------------+
        public async Task<List<ProductQRY>> GetAllProductsAsync()
        {
            var result = await _productRepository.GetAllProductsAsync();
            return result;
        }
        public async Task<bool> CreateProductAsync(ProductCreateDTO productDTO, string? username)
        {
            using(var _context2 = new EcommerceContext())
            using(var _context3 = new EcommerceContext())
            {
                var supplierTask = _setupRepository.GetSupplierByNameAsync(productDTO.Supplier);
                var brandTask = _context2.Brands.FirstOrDefaultAsync(i => i.Name == productDTO.Brand);
                var seasonTask = _context3.Seasons.FirstOrDefaultAsync(i => i.Name == productDTO.Season);

                await Task.WhenAll(supplierTask, brandTask, seasonTask);

                var supplier = supplierTask.Result;
                var brand = brandTask.Result;
                var season = seasonTask.Result;

                Product product = new Product
                {
                    Code = productDTO.Code,
                    Name = productDTO.Name,
                    Cost = productDTO.Cost,
                    Price = productDTO.Price,
                    Discount = productDTO.Discount,
                    Note = productDTO.Note,
                    Year = productDTO.Year,
                    SupplierId = supplier.Id,
                    BrandId = brand.Id,
                    SeasonId = season.Id,
                    IsActive = productDTO.IsActive,
                    UpdatedBy = username
                };

                await _productRepository.CreateProductAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> UpdateProductAsync(ProductUpdateDTO productDTO, string? username)
        {
            using (var _context2 = new EcommerceContext())
            using (var _context3 = new EcommerceContext())
            using (var _context4 = new EcommerceContext())
            {
                var productTask = _productRepository.GetProductByIdAsync(productDTO.Id);
                var supplierTask = _context2.Suppliers.FirstOrDefaultAsync(i => i.Name == productDTO.Supplier); 
                var brandTask = _context3.Brands.FirstOrDefaultAsync(i => i.Name == productDTO.Brand);
                var seasonTask = _context4.Seasons.FirstOrDefaultAsync(i => i.Name == productDTO.Season);

                await Task.WhenAll(productTask, supplierTask, brandTask, seasonTask);

                var product = productTask.Result;
                var supplier = supplierTask.Result;
                var brand = brandTask.Result;
                var season = seasonTask.Result;

                product.Code = productDTO.Code;
                product.Name = productDTO.Name;
                product.Cost = productDTO.Cost;
                product.Price = productDTO.Price;
                product.Discount = productDTO.Discount;
                product.Note = productDTO.Note;
                product.Year = productDTO.Year;
                product.SupplierId = supplier.Id;
                product.BrandId = brand.Id;
                product.SeasonId = season.Id;
                product.IsActive = productDTO.IsActive;
                product.UpdatedBy = username;
                await _productRepository.UpdateProductAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        //+------------------------------------------------------------------+
        //| Product Content                                     
        //+------------------------------------------------------------------+
        public async Task<List<ProductContentQRY>> GetAllProductContentsAsync()
        {
            var result = await _productRepository.GetAllProductContentsAsync();
            return result;
        }
        public async Task<List<byte[]>> GetProductMediaAsync(int productId)
        {
            var result = await _productRepository.GetProductMediaUrlsByProductIdAsync(productId);
            List<byte[]> mediaList = new List<byte[]>();
            foreach(var mediaUrl in result)
            {
                mediaList.Add(await Helpers.GetFileByUrlAsync(mediaUrl));
            }
            return mediaList;
        }
        public async Task<bool> CreateProductContentAsync(ProductContentCreateDTO productDTO, string? username)
        {
            var product =  _productRepository.GetProductByIdAsync(productDTO.ProductId);
            var productInfoExist =  _productRepository.GetProductInfoByProductIdAsync(productDTO.ProductId);
            Task.WhenAll(product, productInfoExist);
            if (product == null ) 
            {
                throw new ArgumentException("Product doesn't exist.");
            }
            if(productInfoExist != null)
            {
                throw new ArgumentException("Product content already exist you can update it.");
            }
            using (var _context2 = new EcommerceContext())
            using (var _context3 = new EcommerceContext())
            {
                var productInfo = new ProductInfo
                {
                    ProductId = productDTO.ProductId,
                    ShortDescription = productDTO.ShortDescription,
                    LongDescription = productDTO.LongDescription,
                    Weight = productDTO.Weight,
                    ShippingWeight = productDTO.ShippingWeight,
                    MinOrder = productDTO.MinOrder,
                    MaxOrder = productDTO.MaxOrder,
                    UpdatedBy = username
                };

                var categoryIdsTask = _context2.Categories
                    .Where(c => productDTO.Categories.Contains(c.Name))
                    .ToDictionaryAsync(c => c.Name, c => c.Id);

                var tagIdsTask = _context3.Tags
                    .Where(c => productDTO.Tags.Contains(c.Name))
                    .ToDictionaryAsync(c => c.Name, c => c.Id);

                var productMediaTasks = productDTO.Media.Select(file =>
                {
                    return Task.Run(async () =>
                    {
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                        var parts = fileNameWithoutExtension.Split('_');
                        var color = parts.Length > 1 ? parts.Last().ToLower() : "unknown";
                        var fileExtension = Path.GetExtension(file.FileName);
                        var uniqueFileName = $"{Guid.NewGuid()}_{color}{fileExtension}";
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "product-media", uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        string mediaType;
                        if (Constants.ImageExtensions.Contains(fileExtension))
                        {
                            mediaType = FileTypeEnum.Image.ToString();
                        }
                        else if (Constants.VideoExtensions.Contains(fileExtension))
                        {
                            mediaType = FileTypeEnum.Video.ToString();
                        }
                        else
                        {
                            mediaType = FileTypeEnum.Unknown.ToString();
                        }

                        return new ProductMedium
                        {
                            ProductId = productDTO.ProductId,
                            MediaType = mediaType,
                            Url = filePath,
                            AltText = productDTO.ShortDescription,
                            UpdatedBy = username
                        };
                    });
                }).ToList();

                await Task.WhenAll(categoryIdsTask, tagIdsTask);
                var categoryIds = await categoryIdsTask;
                var tagIds = await tagIdsTask;

                var productCategoriesTask = Task.Run(() =>
                    productDTO.Categories
                    .Where(category => categoryIds.TryGetValue(category, out var categoryId))
                    .Select(category => new ProductCategory
                    {
                        ProductId = productDTO.ProductId,
                        CategoryId = categoryIds[category],
                        UpdatedBy = username
                    })
                    .ToList()
                );
                var productTagsTask = Task.Run(() =>
                    productDTO.Tags
                    .Where(tag => tagIds.TryGetValue(tag, out var tagId))
                    .Select(tag => new ProductTag
                    {
                        ProductId = productDTO.ProductId,
                        TagId = tagIds[tag],
                        UpdatedBy = username
                    })
                    .ToList()
                );
                var productCategories = await productCategoriesTask;
                var productTags = await productTagsTask;
                var productMedia = await Task.WhenAll(productMediaTasks);

                var productInfoTask = _productRepository.CreateProductInfoAsync(productInfo);
                var productCategoryTask = _productRepository.CreateProductCategoryRangeAsync(productCategories);
                var productTagTask = _productRepository.CreateProductTagRangeAsync(productTags);
                var productMediaDbTask = _productRepository.CreateProductMediaRangeAsync(productMedia);
                await Task.WhenAll(productInfoTask, productCategoryTask, productTagTask, productMediaDbTask);

                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> UpdateProductContentAsync(ProductContentUpdateDTO productDTO, string? username)
        {
            List<Task<ProductMedium>> productMediaTasks = new List<Task<ProductMedium>>();
            var productInfo = await  _productRepository.GetProductInfoByProductIdAsync(productDTO.ProductId);
  
            if (productInfo == null)
            {
                throw new ArgumentException("Product content doesn't exist create a new one.");
            }

            var deleteTagsTask = _productRepository.DeleteProductTagsByProductIdAsync(productDTO.ProductId);
            var deleteCategoriesTask = _productRepository.DeleteProductCategoriesByProductIdAsync(productDTO.ProductId);
            if (productDTO?.Media != null)
            {
                var mediaUrls = await _productRepository.GetProductMediaUrlsByProductIdAsync(productDTO.ProductId);
                var deleteMediaTask = _productRepository.DeleteProductMediaByProductIdAsync(productDTO.ProductId);

                foreach (var mediaUrl in mediaUrls) 
                {
                    File.Delete(mediaUrl);
                }
                productMediaTasks = productDTO.Media.Select(file =>
                {
                    return Task.Run(async () =>
                    {
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                        var parts = fileNameWithoutExtension.Split('_');
                        var color = parts.Length > 1 ? parts.Last().ToLower() : "unknown";
                        var fileExtension = Path.GetExtension(file.FileName);
                        var uniqueFileName = $"{Guid.NewGuid()}_{color}{fileExtension}";
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "product-media", uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        string mediaType;
                        if (Constants.ImageExtensions.Contains(fileExtension))
                        {
                            mediaType = FileTypeEnum.Image.ToString();
                        }
                        else if (Constants.VideoExtensions.Contains(fileExtension))
                        {
                            mediaType = FileTypeEnum.Video.ToString();
                        }
                        else
                        {
                            mediaType = FileTypeEnum.Unknown.ToString();
                        }

                        return new ProductMedium
                        {
                            ProductId = productDTO.ProductId,
                            MediaType = mediaType,
                            Url = filePath,
                            AltText = productDTO.ShortDescription,
                            UpdatedBy = username
                        };
                    });
                }).ToList();

                await Task.WhenAll(deleteTagsTask, deleteCategoriesTask, deleteMediaTask);
            }
           
            using (var _context2 = new EcommerceContext())
            using (var _context3 = new EcommerceContext())
            {
                productInfo.ProductId = productDTO.ProductId;
                productInfo.ShortDescription = productDTO.ShortDescription;
                productInfo.LongDescription = productDTO.LongDescription;
                productInfo.Weight = productDTO.Weight;
                productInfo.ShippingWeight = productDTO.ShippingWeight;
                productInfo.MinOrder = productDTO.MinOrder;
                productInfo.MaxOrder = productDTO.MaxOrder;
                productInfo.UpdatedBy = username;

                var categoryIdsTask = _context2.Categories
                    .Where(c => productDTO.Categories.Contains(c.Name))
                    .ToDictionaryAsync(c => c.Name, c => c.Id);

                var tagIdsTask = _context3.Tags
                    .Where(c => productDTO.Tags.Contains(c.Name))
                    .ToDictionaryAsync(c => c.Name, c => c.Id);

         
                await Task.WhenAll(categoryIdsTask, tagIdsTask);
                var categoryIds = await categoryIdsTask;
                var tagIds = await tagIdsTask;

                var productCategoriesTask = Task.Run(() =>
                    productDTO.Categories
                    .Where(category => categoryIds.TryGetValue(category, out var categoryId))
                    .Select(category => new ProductCategory
                    {
                        ProductId = productDTO.ProductId,
                        CategoryId = categoryIds[category],
                        UpdatedBy = username
                    })
                    .ToList()
                );
                var productTagsTask = Task.Run(() =>
                    productDTO.Tags
                    .Where(tag => tagIds.TryGetValue(tag, out var tagId))
                    .Select(tag => new ProductTag
                    {
                        ProductId = productDTO.ProductId,
                        TagId = tagIds[tag],
                        UpdatedBy = username
                    })
                    .ToList()
                );
                var productCategories = await productCategoriesTask;
                var productTags = await productTagsTask;
                var productMedia = await Task.WhenAll(productMediaTasks);

                var productInfoTask = _productRepository.UpdateProductInfoAsync(productInfo);
                var productCategoryTask = _productRepository.CreateProductCategoryRangeAsync(productCategories);
                var productTagTask = _productRepository.CreateProductTagRangeAsync(productTags);
                var productMediaDbTask = _productRepository.CreateProductMediaRangeAsync(productMedia);
                await Task.WhenAll(productInfoTask, productCategoryTask, productTagTask, productMediaDbTask);

                await _context.SaveChangesAsync();
                return true;
            }
        }

        //+------------------------------------------------------------------+
        //| Transcation                             
        //+------------------------------------------------------------------+
        public async Task<List<TransactionQRY>> GetAllTransactionsAsync()
        {
            var result = await _productRepository.GetAllTransactionsAsync();
            return result;
        }
        public async Task<bool> CreateTransactionAsync(TransactionCreateDTO transactionDTO, string? username)
        {
            Transaction transaction = new Transaction
            {
                ProductId = transactionDTO.ProductId,
                Quantity = transactionDTO.Quantity,
                TransactionType = transactionDTO.TransactionType.ToString(),
                Note = transactionDTO.Note,
                UpdatedBy = username,
                TransactionAttributes = new List<TransactionAttribute>(),
            };
            foreach (var item in transactionDTO.transactionAttributes) 
            {
                transaction.TransactionAttributes.Add(new TransactionAttribute
                {
                    Attribute = item.Name,
                    AttributeOption = item.Option,
                });
            }
            transaction.ProductAttributeKey = await Helpers.GenerateProductAttributeKeyAsync(transaction.ProductId, transaction.TransactionAttributes);

            await _productRepository.CreateTransactionAsync(transaction);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
