namespace ecommerce_dash_api.Areas.Shop.QRYS
{
    public class HomePageQRY
    {
        public byte[]? SectionImage { get; set; }
        public List<CategoryProductQRY> categoryProducts { get; set; } = new List<CategoryProductQRY>();   
    }
}
