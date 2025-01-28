using ecommerce_dash_api.Models;

namespace ecommerce_dash_api.Areas.Dashboard.QRYS
{
    public class GroupProductQuantityAttributes
    {
        public int ProductQuantityId { get; set; }
        public List<ProductQuantityAttribute> productQuantityAttrbiutes { get; set; }

    }
}
