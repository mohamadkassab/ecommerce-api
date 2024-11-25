using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class SectionCreateDTO
    {
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name { get; set; }
        public List<int> Categories { get; set; }
    }
}
