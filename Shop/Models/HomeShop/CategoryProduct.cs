using Shop.Models.Base;

namespace Shop.Models.HomeShop
{
    public class CategoryProduct:BaseEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Product? Product { get; set; }
        public Category? Category { get; set; }
    }
}
