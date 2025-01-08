using System.ComponentModel.DataAnnotations;
using ecommerce_dash_api.Enum;

namespace ecommerce_dash_api.Areas.Dashboard.DTOS
{
    public class ShippingMCreateDTO
    {
        private string _name = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name
        {
            get => _name;
            set => _name = value.Trim();
        }

        [Required]
        [MaxFileSize(10 * 1024 * 1024)]
        [FileTypeValidation(FileTypeEnum.Image)]
        public IFormFile IconFile { get; set; } = null!;

        [Required]
        public Boolean Overseas { get; set; }

        [Required]
        public Boolean IsActive { get; set; }
    }
}
