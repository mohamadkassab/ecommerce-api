using ecommerce_dash_api.Enum;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class TransactionCreateDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string TransactionType { get; set; } = null!;

        public string Note { get; set; } = string.Empty;

        public List<TransactionAttributeDTO> transactionAttributes { get; set; } = new List<TransactionAttributeDTO>();
    }
}
