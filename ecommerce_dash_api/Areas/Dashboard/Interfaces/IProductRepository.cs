using ecommerce_dash_api.Areas.Dashboard.QRYS;
using ecommerce_dash_api.Models;

namespace ecommerce_dash_api.Areas.Dashboard.Interfaces
{
    public interface IProductRepository
    {
        //+------------------------------------------------------------------+
        //| Product                                            
        //+------------------------------------------------------------------+
        Task<Product?> GetProductByIdAsync(int id);
        Task<List<ProductQRY>> GetAllProductsAsync();
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);

        //+------------------------------------------------------------------+
        //| Product Content                                         
        //+------------------------------------------------------------------+
        Task<List<ProductContentQRY>> GetAllProductContentsAsync();

        //+------------------------------------------------------------------+
        //| Product Category                                         
        //+------------------------------------------------------------------+
        Task<ProductCategory?> GetProductCategoryByIdAsync(int id);
        Task CreateProductCategoryAsync(ProductCategory productCategory);
        Task CreateProductCategoryRangeAsync(List<ProductCategory> productCategoryRange);
        Task UpdateProductCategoryAsync(ProductCategory productCategory);
        Task DeleteProductCategoriesByProductIdAsync(int productId);

        //+------------------------------------------------------------------+
        //| Product Info                                           
        //+------------------------------------------------------------------+
        Task<ProductInfo?> GetProductInfoByProductIdAsync(int id);
        Task CreateProductInfoAsync(ProductInfo productInfo);
        Task CreateProductInfoRangeAsync(List<ProductInfo> productInfoRange);
        Task UpdateProductInfoAsync(ProductInfo productInfo);

        //+------------------------------------------------------------------+
        //| Product Media                                           
        //+------------------------------------------------------------------+
        Task<List<string>?> GetProductMediaUrlsByProductIdAsync(int productId);
        Task CreateProductMediaAsync(ProductMedium productMedia);
        Task CreateProductMediaRangeAsync(IEnumerable<ProductMedium> productMediaRange);
        Task UpdateProductMediaAsync(ProductMedium productMedia);
        Task DeleteProductMediaByProductIdAsync(int productId);

        //+------------------------------------------------------------------+
        //| Product Tag                                           
        //+------------------------------------------------------------------+
        Task<ProductTag?> GetProductTagByIdAsync(int id);
        Task CreateProductTagAsync(ProductTag productTag);
        Task CreateProductTagRangeAsync(List<ProductTag> productTagRange);
        Task UpdateProductTagAsync(ProductTag productTag);
        Task DeleteProductTagsByProductIdAsync(int productId);

        //+------------------------------------------------------------------+
        //| Transaction                                          
        //+------------------------------------------------------------------+
        Task<List<TransactionQRY>> GetAllTransactionsAsync();
        Task CreateTransactionAsync(Transaction transaction);
    }
}
