using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class CurrencyCreateDTO
    {
        private string _name = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name
        {
            get => _name;
            set => _name = value.Trim().ToUpper();
        }

        private string _symbol = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Symbol must be between 1 and 255 characters.")]
        public string Symbol
        {
            get => _symbol;
            set => _symbol = value.Trim().ToUpper();
        }

        [Required]
        public decimal ExchangeRateUsd { get; set; }


        [Required]
        public string Country { get; set; } = null!;
    }
}
