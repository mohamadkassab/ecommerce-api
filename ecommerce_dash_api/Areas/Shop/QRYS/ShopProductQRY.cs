using System.Text.Json.Serialization;

namespace ecommerce_dash_api.Areas.Shop.QRYS
{
    public class ShopProductQRY
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Note { get; set; } = string.Empty;
        public string Supplier { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Season { get; set; } = null!;
        public int Year { get; set; }
        public string ShortDescription { get; set; } = null!;
        public string LongDescription { get; set; } = null!;
        public decimal Weight { get; set; }
        public decimal ShippingWeight { get; set; }
        public short MinOrder { get; set; }
        public short MaxOrder { get; set; }
        public byte[]? Media { get; set; }

        [JsonIgnore]
        public string? MediaUrl { get; set; } = string.Empty;
    }
}
