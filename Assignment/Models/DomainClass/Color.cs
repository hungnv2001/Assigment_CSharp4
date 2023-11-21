using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.DomainClass
{
    public class Color
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set;}
    }
}
