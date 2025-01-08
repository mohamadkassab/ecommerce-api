using ecommerce_dash_api.Models;

namespace ecommerce_dash_api.Areas.Dashboard.QRYS
{
    public class BrandQRY
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Website { get; set; } = string.Empty;
        public byte[]? LogoFile { get; set; }
        public string Country { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
