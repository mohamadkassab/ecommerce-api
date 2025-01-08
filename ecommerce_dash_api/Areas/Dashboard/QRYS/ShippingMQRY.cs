using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.Areas.Dashboard.QRYS
{
    public class ShippingMQRY
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte[]? IconFile { get; set; }
        public Boolean Overseas { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public Boolean IsActive { get; set; }
    }
}
