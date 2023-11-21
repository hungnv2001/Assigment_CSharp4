using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class ProductImg
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductID {  get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }
        [MaxLength(100)]
        public string Url1 { get; set; }
        [MaxLength(100)]
        public string Url2 { get; set; }
        [MaxLength(100)]
        public string Url3 { get; set; }
    }
}
