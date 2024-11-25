namespace ecommerce_dash_api.QRYS
{
    public class BrandQRY
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Website { get; set; }
        public string? LogoUrl { get; set; }
        public int? CountryId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
