namespace ecommerce_dash_api.QRYS
{
    public class CurrencyQRY
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public decimal ExchangeRateUsd { get; set; }
        public int? CountryId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

    }
}
