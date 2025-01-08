namespace ecommerce_dash_api.Areas.Dashboard.QRYS
{
    public class ProductContentQRY
    {
        public int ProductId { get; set; }
        public string ShortDescription { get; set; } = null!;
        public string LongDescription { get; set; } = null!;
        public decimal Weight { get; set; }
        public decimal ShippingWeight { get; set; }
        public short MinOrder { get; set; }
        public short MaxOrder { get; set; }
        public List<string> Categories { get; set; } = null!;
        public List<string> Tags { get; set; } = null!;
        public List<string> MediaUrls { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
