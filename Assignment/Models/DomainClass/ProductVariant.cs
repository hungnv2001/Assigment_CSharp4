using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductID {  get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
        [Required]
        public int ColorID {  get; set; }
        [ForeignKey("ColorID")]
        public Color Color { get; set; }
        [Required]
        public int SizeID {  get; set; }
        [ForeignKey("SizeID")]
        public Size Size { get; set; }
        public int Status {  get; set; }
        public ICollection<InvoiceItem> Invoices { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
