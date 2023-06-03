using Shop.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models.HomeShop
{
    public class Product : BaseEntity
    {
        public string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public double InitialPrice { get; set; }
        public double FinalPrice { get; set; }
        public int Quantity { get; set; }
        public ICollection<CategoryProduct>? CategoryProducts { get; set; }
    }
}
