using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public float TotalPrice {  get; set; }
        [Required]
        public DateTime BoughtAt { get; set; }
        public int Status {  get; set; }
        [Required]
        public int CustomerID {  get; set; }

        [ForeignKey("CustomerID")]
        public User User { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
