using ecommerce_dash_api.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class UpdateChartDTO
    {
        public int Id { get; set; } 

        [Required, StringLength(255, MinimumLength = 1, ErrorMessage = "Label must be between 1 and 255 characters.")]
        public string Label { get; set; } = null!;
        public string? Query { get; set; }

        [AtLeastOneRequired<ChartPropertyDTO>]
        public List<ChartPropertyDTO> ChartProperties { get; set; } = null!;
    }
}
