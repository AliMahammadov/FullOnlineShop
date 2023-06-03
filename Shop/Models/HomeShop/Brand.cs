using Shop.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models.HomeShop
{
    public class Brand:BaseEntity
    {
        public string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
 