using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.Areas.Dashboard.DTOS
{
    public class CountryUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        private string _name = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name
        {
            get => _name;
            set => _name = value.Trim();
        }

        private string _code = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 2, ErrorMessage = "Code  must be between 2 and 255 characters.")]
        public string Code
        {
            get => _code;
            set => _code = value.Trim();
        }
    }
}
