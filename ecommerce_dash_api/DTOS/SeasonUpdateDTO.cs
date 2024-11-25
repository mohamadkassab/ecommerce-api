using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class SeasonUpdateDTO
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name { get; set; }
    }
}
