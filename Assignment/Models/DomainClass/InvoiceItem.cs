using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class InvoiceItem
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Tên sản phẩm")]
        public Guid ProductVariantId {  get; set; }
        [ForeignKey("ProductVariantId")]
        public ProductVariant ProductVariant { get; set; }
        [Required]
       
        public Guid InvoiceID {  get; set; }
        [ForeignKey("InvoiceID")]
        public Invoice Invoice { get; set; }
        [Required]
        [DisplayName("Giá bán")]
        public float Price {  get; set; }
        [Required]
        [DisplayName("Số lượng")]
        public int Quantity {  get; set; }
        [DisplayName("Trạng thái")]
        public int Status {  get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
