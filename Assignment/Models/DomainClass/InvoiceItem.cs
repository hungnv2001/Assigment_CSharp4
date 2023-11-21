using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductVariantId {  get; set; }
        [ForeignKey("ProductVariantId")]
        public ProductVariant ProductVariant { get; set; }
        [Required]
        public int InvoiceID {  get; set; }
        [ForeignKey("InvoiceID")]
        public Invoice Invoice { get; set; }
        [Required]
        public float Price {  get; set; }
        [Required]
        public int Quantity {  get; set; }
        public int Status {  get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
