using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class AttributeCreateDTO
    {
        private string _name = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name
        {
            get => _name;
            set => _name = value.Trim().ToUpper();
        }
        public List<string> Options { get; set; } = new List<string>();
    }
}
