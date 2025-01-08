namespace ecommerce_dash_api.Areas.Shop.QRYS
{
    public class CategoryProductQRY
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public List<ShopProductQRY> Products { get; set; } = new List<ShopProductQRY>();
    }
}
