using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class CountryUpdateDTO
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Country must be between 1 and 255 characters.")]
        public string Name { get; set; }

        private string _code = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 2, ErrorMessage = "Code  must be between 2 and 255 characters.")]
        public string Code
        {
            get => _code;
            set => _code = value.ToUpper();
        }
    }
}
