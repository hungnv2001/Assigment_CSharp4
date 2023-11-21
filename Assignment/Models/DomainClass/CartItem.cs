using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Số lượng")]
        public int Quantiy {  get; set; }
        [DisplayName("Tên sản phẩm")]
        public Guid ProductVariantID {  get; set; }
        [ForeignKey("ProductVariantID")]
        public ProductVariant ProductVariant { get; set; }
        [Required]

        public Guid CartID {  get; set; }
        [ForeignKey("CartID")]
        public Cart Cart { get; set; }
    }
}
