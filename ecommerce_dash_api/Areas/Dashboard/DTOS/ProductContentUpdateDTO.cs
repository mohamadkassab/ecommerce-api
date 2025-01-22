using System.ComponentModel.DataAnnotations;
using ecommerce_dash_api.Enum;

namespace ecommerce_dash_api.Areas.Dashboard.DTOS
{
    public class ProductContentUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        private string _shortDescription = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "ShortDescription must be between 1 and 255 characters.")]
        public string ShortDescription
        {
            get => _shortDescription;
            set => _shortDescription = value.Trim();
        }

        private string _longDescription = null!;
        [Required(AllowEmptyStrings = false), StringLength(16380, MinimumLength = 1, ErrorMessage = "LongDescription must be between 1 and 16380 characters.")]
        public string LongDescription
        {
            get => _longDescription;
            set => _longDescription = value.Trim();
        }

        [Required]
        [RangeValidation("-999.999", "999.999")]
        public decimal Weight { get; set; }

        [Required]
        [RangeValidation("-999.999", "999.999")]
        public decimal ShippingWeight { get; set; }

        [Required]
        public short MinOrder { get; set; }

        [Required]
        public short MaxOrder { get; set; }

        [Required]
        public List<string> Categories { get; set; } = null!;

        [MaxFileSize(5 * 1024 * 1024)]
        [FileTypeValidation(FileTypeEnum.ImageVideo)]
        public List<IFormFile>? Media { get; set; } 

    }
}
