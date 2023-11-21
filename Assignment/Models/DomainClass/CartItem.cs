using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantiy {  get; set; }
        public int ProductVariantID {  get; set; }
        [ForeignKey("ProductVariantID")]
        public ProductVariant ProductVariant { get; set; }
        [Required]
        public int CartID {  get; set; }
        [ForeignKey("CartID")]
        public Cart Cart { get; set; }
    }
}
