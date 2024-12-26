using System.ComponentModel.DataAnnotations;
using ecommerce_dash_api.Enum;

namespace ecommerce_dash_api.DTOS
{
    public class BrandUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        private string _name = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name
        {
            get => _name;
            set => _name = value.Trim().ToUpper();
        }

        public string Website { get; set; } =string.Empty;

        [MaxFileSize(10 * 1024 * 1024)]
        [FileTypeValidation(FileTypeEnum.Image)]
        public IFormFile? LogoFile { get; set; }

        [Required]
        public string Country { get; set; } = null!;
    }
}
