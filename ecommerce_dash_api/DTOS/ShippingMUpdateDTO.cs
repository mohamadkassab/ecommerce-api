using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class ShippingMUpdateDTO
    {
        public int Id { get; set; }

        private string _name = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name
        {
            get => _name;
            set => _name = value.Trim();
        }

        [MaxFileSize(10 * 1024 * 1024)]
        public IFormFile? IconFile { get; set; }

        [Required]
        public Boolean IsActive { get; set; }
    }
}
