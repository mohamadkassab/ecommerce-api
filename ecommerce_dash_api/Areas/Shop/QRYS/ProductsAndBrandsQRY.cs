namespace ecommerce_dash_api.Areas.Shop.QRYS
{
    public class ProductsAndBrandsQRY
    {
        public List<CategoryProductQRY> categories { get; set; } = null!;
        public List<ShopBrandQRY> brands { get; set; } = null!;
    }
}
