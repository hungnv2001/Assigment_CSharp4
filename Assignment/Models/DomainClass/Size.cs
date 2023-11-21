using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.DomainClass
{
    public class Size
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int Status { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
