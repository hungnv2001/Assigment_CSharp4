using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.DomainClass
{
    public class Size
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Tên size")]
        public string Name { get; set; }
        [DisplayName("Trạng thái")]
        public int Status { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
