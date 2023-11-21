using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Tổng giá trị")]
        public float TotalPrice {  get; set; }
        [Required]
        [DisplayName("Ngày đặt")]
        public DateTime BoughtAt { get; set; }
        [DisplayName("Trạng thái")]
        public int Status {  get; set; }
        [Required]
        [DisplayName("Tên khách hàng")]
        public Guid CustomerID {  get; set; }

        [ForeignKey("CustomerID")]
        public User User { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
