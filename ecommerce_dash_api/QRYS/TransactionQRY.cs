using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Enum;

namespace ecommerce_dash_api.QRYS
{
    public class TransactionQRY
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public List<TransactionAttributeDTO> transactionAttributes { get; set; } = new List<TransactionAttributeDTO>();
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
