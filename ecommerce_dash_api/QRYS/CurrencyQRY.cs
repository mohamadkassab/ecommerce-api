namespace ecommerce_dash_api.QRYS
{
    public class CurrencyQRY
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public decimal ExchangeRateUsd { get; set; }
        public string Country { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public Boolean IsActive { get; set; }
    }
}
