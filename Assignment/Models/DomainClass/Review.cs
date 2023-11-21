using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid InvoiceItemID {  get; set; }
        [ForeignKey("InvoiceItemID")]
        public InvoiceItem InvoiceItem { get; set; }
        [Required]
        public int Rate {  get; set; }
        [Required]
        public string Comment {  get; set; }
    }
}
