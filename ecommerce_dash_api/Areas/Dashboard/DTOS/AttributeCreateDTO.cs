using System.ComponentModel.DataAnnotations;
using ecommerce_dash_api.Utils;

namespace ecommerce_dash_api.Areas.Dashboard.DTOS
{
    public class AttributeCreateDTO
    {
        private string _name = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name
        {
            get => _name;
            set => _name = Helpers.CapitalizeFirstLetter(value.Trim());
        }

        private List<string> _options = new List<string>();
        public List<string> Options
        {
            get => _options;
            set => _options = value.Select(option => Helpers.CapitalizeFirstLetter(option.Trim())).ToList();
        }
    }
}
