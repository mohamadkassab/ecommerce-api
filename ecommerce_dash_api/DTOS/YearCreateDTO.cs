using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class YearCreateDTO
    {
        [Required]
        public int Name { get; set; }
    }
}
