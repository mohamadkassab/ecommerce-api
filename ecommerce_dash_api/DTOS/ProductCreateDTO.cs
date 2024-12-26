using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class ProductCreateDTO
    {
        private string _code = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Code must be between 1 and 255 characters.")]
        public string Code
        {
            get => _code;
            set => _code = value.Trim();
        }

        private string _name = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name
        {
            get => _name;
            set => _name = value.Trim();
        }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public string Supplier { get; set; } = null!;

        [Required]
        public string Brand { get; set; } = null!;

        [Required]
        public int Year { get; set; }

        [Required]
        public string Season { get; set; } = null!;

        [StringLength(255, ErrorMessage = "Note must be between 1 and 255 characters.")]
        public string Note { get; set; } = string.Empty;

        [Required]
        public Boolean IsActive { get; set; }
    }
}
