using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.Areas.Dashboard.DTOS
{
    public class CategoryUpdateDTO
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
    }
}