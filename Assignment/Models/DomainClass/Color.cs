using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.DomainClass
{
    public class Color
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Tên màu")]
        public string Name { get; set; }
        [DisplayName("Trạng thái")]
        public int Status {  get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set;}
    }
}
