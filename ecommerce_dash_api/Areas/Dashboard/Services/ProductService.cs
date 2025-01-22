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
            using(var _context4 = new EcommerceContext())
            {
                var product_attributes = new List<ProductAttribute>();
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

                foreach( var attribute in productDTO.attributes)
                {
                    product_attributes.Add(new ProductAttribute
                    {
                        Product = product,
                        Attribute = attribute,
                        UpdatedBy= username,

                    });
                }
                await _productRepository.CreateProductAsync(product);
                await _productRepository.CreateProductAttributeRangeAsync(product_attributes);
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
                var product_attributes = new List<ProductAttribute>();
                var productTask = _productRepository.GetProductByIdAsync(productDTO.Id);
                var supplierTask = _context2.Suppliers.FirstOrDefaultAsync(i => i.Name == productDTO.Supplier); 
                var brandTask = _context3.Brands.FirstOrDefaultAsync(i => i.Name == productDTO.Brand);
                var seasonTask = _context4.Seasons.FirstOrDefaultAsync(i => i.Name == productDTO.Season);

                await Task.WhenAll(productTask, supplierTask, brandTask, seasonTask);
                await _productRepository.DeleteProductAttributeByProductIdAsync(productDTO.Id);

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

                foreach (var attribute in productDTO.attributes)
                {
                    product_attributes.Add(new ProductAttribute
                    {
                        Product = product,
                        Attribute = attribute,
                        UpdatedBy = username,

                    });
                }

                await _productRepository.UpdateProductAsync(product);
                await _productRepository.CreateProductAttributeRangeAsync(product_attributes);
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
            using (var _context2 = new EcommerceContext())
            {
                var product =  _productRepository.GetProductByIdAsync(productDTO.ProductId);
                var productInfoExist = _context2.ProductInfos.FirstOrDefaultAsync(i => i.ProductId == productDTO.ProductId);
                await Task.WhenAll(product, productInfoExist);
                if (product.Result == null ) 
                {
                    throw new ArgumentException("Product doesn't exist");
                }
                if(productInfoExist.Result != null)
                {
                    throw new ArgumentException("Product content already exist you can update it");
                }

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

                var categoryNames = productDTO.Categories[0]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(name => name.Trim())
                    .ToList();

                var categoryIds = await _context2.Categories
                    .Where(c => categoryNames.Contains(c.Name))
                    .Select(c => c.Id)
                    .ToListAsync();

                var productCategoriesTask = Task.Run(() =>
                    categoryIds
                        .Select(categoryId => new ProductCategory
                        {
                            ProductId = productDTO.ProductId,
                            CategoryId = categoryId,
                            UpdatedBy = username
                        })
                        .ToList()
                );


                var productCategories = await productCategoriesTask;
                var productMedia = await Task.WhenAll(productMediaTasks);

                var productInfoTask = _productRepository.CreateProductInfoAsync(productInfo);
                var productCategoryTask = _productRepository.CreateProductCategoryRangeAsync(productCategories);
                var productMediaDbTask = _productRepository.CreateProductMediaRangeAsync(productMedia);
                await Task.WhenAll(productInfoTask, productCategoryTask, productMediaDbTask);

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

                await Task.WhenAll(deleteCategoriesTask, deleteMediaTask);
            }
           
            using (var _context2 = new EcommerceContext())
            {
                productInfo.ProductId = productDTO.ProductId;
                productInfo.ShortDescription = productDTO.ShortDescription;
                productInfo.LongDescription = productDTO.LongDescription;
                productInfo.Weight = productDTO.Weight;
                productInfo.ShippingWeight = productDTO.ShippingWeight;
                productInfo.MinOrder = productDTO.MinOrder;
                productInfo.MaxOrder = productDTO.MaxOrder;
                productInfo.UpdatedBy = username;

                var categoryNames = productDTO.Categories[0]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(name => name.Trim()) 
                    .ToList();

                var categoryIds = await _context2.Categories
                    .Where(c => categoryNames.Contains(c.Name))
                    .Select(c => c.Id)
                    .ToListAsync();

                var productCategoriesTask = Task.Run(() =>
                    categoryIds
                        .Select(categoryId => new ProductCategory
                        {
                            ProductId = productDTO.ProductId,
                            CategoryId = categoryId,
                            UpdatedBy = username
                        })
                        .ToList()
                );
                var productCategories = await productCategoriesTask;
                var productMedia = await Task.WhenAll(productMediaTasks);

                var productInfoTask = _productRepository.UpdateProductInfoAsync(productInfo);
                var productCategoryTask = _productRepository.CreateProductCategoryRangeAsync(productCategories);
                var productMediaDbTask = _productRepository.CreateProductMediaRangeAsync(productMedia);
                await Task.WhenAll(productInfoTask, productCategoryTask, productMediaDbTask);

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
            using (var _context2 = new EcommerceContext()) 
            {
                int transactionValue = 0;
                int matchedProductQuantityId = 0;
                var attributesBluePrintTask = _context.ProductAttributes.Where(i => i.ProductId == transactionDTO.ProductId).Select(i => i.Attribute).ToListAsync();
                var productQuantityTask = (from pq in _context2.ProductQuantities
                                           where pq.ProductId == transactionDTO.ProductId
                                           join pqa in _context2.ProductQuantityAttrbiutes on pq.Id equals pqa.ProductQuantityId
                                           group pqa by pq.Id into groupedPqa
                                           select new
                                           {
                                               id = groupedPqa.Key,
                                               productQuantityAttributes = groupedPqa.ToList()
                                           }).ToListAsync();

                Transaction transaction = new Transaction
                {
                    ProductId = transactionDTO.ProductId,
                    Quantity = transactionDTO.Quantity,
                    TransactionType = transactionDTO.TransactionType.ToString(),
                    Note = transactionDTO.Note,
                    UpdatedBy = username,
                    TransactionAttributes = new List<TransactionAttribute>(),
                };

                await Task.WhenAll(attributesBluePrintTask, productQuantityTask);
                var attributesBluePrint = attributesBluePrintTask.Result;
                var productQuantity = productQuantityTask.Result;

                var transactionAttributesTask = transactionDTO.transactionAttributes.Select(async item =>
                {
                    if (!attributesBluePrint.Contains(item.Name))
                    {
                        throw new InvalidOperationException("Please verify the attributes");
                    }
                    transaction.TransactionAttributes.Add(new TransactionAttribute
                    {
                        Attribute = item.Name,
                        AttributeOption = item.Option,
                    });
                });
                var currentTransactionAttributes = transactionDTO.transactionAttributes.Select(i => i).OrderBy(i => i.Name).ToList();
                foreach (var item in productQuantity)
                {
                    int i = 0;
                    int matchingElements = 0;
                    var orderedProductQuantityAttributes = item?.productQuantityAttributes.OrderBy(i => i.Attribute).ToList();

                    if (currentTransactionAttributes.Count == 0 && orderedProductQuantityAttributes?.Count == 0)
                    {
                        matchedProductQuantityId = item.id;
                    }

                    if (currentTransactionAttributes.Count == orderedProductQuantityAttributes?.Count)
                    {
                        bool allMatch = true;

                        foreach (var productQuantityAttribute in orderedProductQuantityAttributes)
                        {
                            if (productQuantityAttribute.Attribute != currentTransactionAttributes[i].Name ||
                                productQuantityAttribute.AttributeOption != currentTransactionAttributes[i].Option)
                            {
                                allMatch = false;
                                break;
                            }
                            matchingElements++;
                            i++;
                        }

                        if (allMatch)
                        {
                            matchedProductQuantityId = item.id;
                        }
                    }
                }

                if ((transactionDTO.TransactionType.ToString() == TransactionTypeEnum.Return.ToString()) || (transactionDTO.TransactionType.ToString() == TransactionTypeEnum.Restock.ToString()))
                {
                    transactionValue = transactionDTO.Quantity;
                }
                else
                {
                    transactionValue = transactionDTO.Quantity * -1;
                }

                if (matchedProductQuantityId == 0) 
                {
                    var newProductQuantity = new ProductQuantity();
                    newProductQuantity.ProductId = transactionDTO.ProductId;
                    newProductQuantity.Quantity = transactionDTO.Quantity;
                    newProductQuantity.ProductQuantityAttrbiutes = new List<ProductQuantityAttrbiute>();

                    foreach (var transactionAttribute in transactionDTO.transactionAttributes)
                    {
                        var productQuantityAttribute = new ProductQuantityAttrbiute
                        {
                            Attribute = transactionAttribute.Name,
                            AttributeOption = transactionAttribute.Option
                        };
                        newProductQuantity.ProductQuantityAttrbiutes.Add(productQuantityAttribute);
                    }
                    await _productRepository.CreateProductQuantityAsync(newProductQuantity);
                }
                else
                {
                    var productQuantityExisting = await _context.ProductQuantities.Where(pq => pq.Id == matchedProductQuantityId).FirstOrDefaultAsync();
                    productQuantityExisting.Quantity += transactionValue;
                    await _productRepository.UpdateProductQuantityAsync(productQuantityExisting);
                }
                await Task.WhenAll(transactionAttributesTask);
                await _productRepository.CreateTransactionAsync(transaction);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
