using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.Areas.Dashboard.DTOS
{
    public class ChartUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        private string _label = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Label must be between 1 and 255 characters.")]
        public string Label
        {
            get => _label;
            set => _label = value.Trim().ToLower();
        }

        public string Query { get; set; } = string.Empty;

        [Required]
        [RequiredNonEmptyList]
        public List<ChartPropertyDTO> ChartProperties { get; set; } = new List<ChartPropertyDTO>();
    }
}
