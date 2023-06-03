using Shop.Models.Base;

namespace Shop.Models.HomeShop
{
    public class ProductImage:BaseEntity
    {
        public string ImgUrl { get; set; }
        public bool? IsPoster { get; set; }
    }
}
