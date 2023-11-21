using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.DomainClass
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Tên thương hiệu")]
        public string Name { get; set; }
        [DisplayName("Trạng thái hoạt động")]
        public int Status {  get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
