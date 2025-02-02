using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.Areas.Shop.DTOS
{
    public class SearchQueryDTO
    {
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Query must be between 1 and 255 characters.")]
        public string Query { get; set; } = null!;
        public int PageNbr { get; set; }
        public int PageSize { get; set; }
        public string? SortingOption { get; set; } = string.Empty;
        public List<string>? Brands { get; set; } = new List<string>();
        public List<string>? Categories { get; set; } = new List<string>();
    }
}
