using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.Areas.Dashboard.DTOS
{
    public class CountryCreateDTO
    {
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name { get; set; } = null!;

        private string _code = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Code  must be between 2 and 255 characters.")]
        public string Code
        {
            get => _code;
            set => _code = value.Trim();
        }
    }
}
