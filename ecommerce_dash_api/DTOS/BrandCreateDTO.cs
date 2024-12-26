using System.ComponentModel.DataAnnotations;
using ecommerce_dash_api.Enum;
using ecommerce_dash_api.Utils;

namespace ecommerce_dash_api.DTOS
{
    public class BrandCreateDTO
    {
        private string _name = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name
        {
            get => _name;
            set => _name = value.Trim().ToUpper();
        }

        public string Website { get; set; } = string.Empty;

        [Required]
        [MaxFileSize(2 * 1024 * 1024)]
        [FileTypeValidation(FileTypeEnum.Image)]
        public IFormFile LogoFile { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;
    }
}
