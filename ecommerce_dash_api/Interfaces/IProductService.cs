using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.QRYS;

namespace ecommerce_dash_api.Interfaces
{
    public interface IProductService
    {
        //+------------------------------------------------------------------+
        //| Product                                            
        //+------------------------------------------------------------------+
        Task<List<ProductQRY>> GetAllProductsAsync();
        Task<bool> CreateProductAsync(ProductCreateDTO productDTO, string? username);
        Task<bool> UpdateProductAsync(ProductUpdateDTO productDTO, string? username);

        //+------------------------------------------------------------------+
        //| Product Content                                
        //+------------------------------------------------------------------+
        Task<List<ProductContentQRY>> GetAllProductContentsAsync();
        Task<List<byte[]>> GetProductMediaAsync(int productId);
        Task<bool> CreateProductContentAsync(ProductContentCreateDTO productDTO, string? username);
        Task<bool> UpdateProductContentAsync(ProductContentUpdateDTO productDTO, string? username);

        //+------------------------------------------------------------------+
        //| Transcation                             
        //+------------------------------------------------------------------+
        Task<List<TransactionQRY>> GetAllTransactionsAsync();
        Task<bool> CreateTransactionAsync(TransactionCreateDTO transactionDTO, string? username);
    }
}
