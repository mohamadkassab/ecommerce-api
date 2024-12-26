using ecommerce_dash_api.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class ChartCreateDTO
    {
        private string _label = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Label must be between 1 and 255 characters.")]
        public string Label
        {
            get => _label;
            set => _label = value.Trim().ToLower();
        }

        public string Query { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Chart type must be between 1 and 255 characters.")]
        public string ChartType { get; set; } = null!;

        public List<ChartPropertyDTO> ChartProperties { get; set; } = new List<ChartPropertyDTO>();
    }
}
