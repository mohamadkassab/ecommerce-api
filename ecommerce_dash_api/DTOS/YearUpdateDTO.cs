using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class YearUpdateDTO
    {
        public int Id { get; set; }

        [Required]
        public int Name { get; set; }
    }
}
