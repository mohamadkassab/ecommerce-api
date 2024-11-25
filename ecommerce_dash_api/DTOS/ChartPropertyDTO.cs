using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class ChartPropertyDTO
    {
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Property name must be between 1 and 255 characters.")]
        public string PropertyName { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Property value must be between 1 and 255 characters.")]
        public string PropertyValue { get; set; }
    }
}
