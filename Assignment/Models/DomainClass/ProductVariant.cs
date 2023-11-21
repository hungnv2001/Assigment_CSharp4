using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class ProductVariant
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Tên sản phẩm")]
        public Guid ProductID {  get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
        [Required]
        public Guid ColorID {  get; set; }
        [ForeignKey("ColorID")]
        [DisplayName("Màu sắc")]
        public Color Color { get; set; }
        [Required]
        [DisplayName("Kích thước")]
        public Guid SizeID {  get; set; }
        [ForeignKey("SizeID")]
        public Size Size { get; set; }
        [DisplayName("Trạng thái")]
        public int Status {  get; set; }
        [DisplayName("Số lượng")]
        public int Quantity {  get; set; }
        public ICollection<InvoiceItem> Invoices { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
