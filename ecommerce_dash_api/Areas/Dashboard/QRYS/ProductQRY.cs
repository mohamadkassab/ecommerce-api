namespace ecommerce_dash_api.Areas.Dashboard.QRYS
{
    public class ProductQRY
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public List<string> attributes { get; set; } = new List<string>();
        public string Supplier { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public int Year { get; set; }
        public string Season { get; set; } = null!;
        public string Note { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public Boolean IsActive { get; set; }
    }
}
